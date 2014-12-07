using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkgMVC.Models;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc.Ajax;
using ParkgMVC;

namespace ParkgMVC.Controllers
{

    [HandleError(View = "~/Views/Shared/Error.aspx")]
    public class VisitController : Controller
    {
        //
        // GET: /Visit/
        MyParkingEntities mp = new MyParkingEntities();
        public ActionResult ListVisit()
        {
            if (User.Identity.IsAuthenticated & User.IsInRole("Driver"))
            {
               // return View(mp.visit.Where(x=>x.place.Status == "Occupied").ToList());
                ViewData["ActiveVisit"]=mp.visit.Where(x => x.DateOut == "dateout" & x.ts.Login == User.Identity.Name).ToList();
                ViewData["NotActiveVisit"] = mp.visit.Where(x => x.DateOut != "dateout" & x.ts.Login == User.Identity.Name).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
            
        }
        
        public ActionResult RegisterIn()
        {
                if (User.Identity.IsAuthenticated & User.IsInRole("Security"))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("LogOn", new { Controller = "Account" });
                }
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterIn(ts numb)
        {
                if (ModelState.IsValidField("Number"))
                {
                    visitparameters vp = mp.visitparameters.Where(x => x.Status == "Active").FirstOrDefault();
                    if (vp != null)
                    {
                        reservation r = new reservation();
                        r.FindOnExpired("");
                        ts searchts = mp.ts.Where(x => x.Number == numb.Number & x.Status == "True").FirstOrDefault();
                        if (searchts != null)
                        {
                            visit nvis = new visit();
                                            visit vis = mp.visit.Where(x => x.id_ts == searchts.id_ts & x.DateOut == "dateout").FirstOrDefault();
                                            if (vis == null)
                                            {
                                                place p = new place();
                                                string Log = searchts.Login;
                                                string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                                                reservation res = mp.reservation.Where(x => x.Login == Log & x.Status == "Active").FirstOrDefault(); //Существует ли бронь
                                                if (res != null)
                                                {
                                                    if (res.id_alternative_location_place != null)
                                                    {

                                                        nvis.RegisterIn((long)res.id_alternative_location_place, searchts.id_ts, Date, vp.id_vis_param);
                                                        res.DateOutFromActivity = Date;
                                                        res.Status = "Closed";
                                                        res.Description = "Reservation was used";
                                                        mp.Entry(res).State = EntityState.Modified;
                                                        mp.SaveChanges();
                                                        r.Revoke("Reservation was used", res, Date);
                                                        p.ChangeStatus("Occupied", (long)res.id_alternative_location_place, (Int32)res.place.id_alternative_tariff_on_place);
                                                    }
                                                    else
                                                    {
                                                        ViewData["ExceptionRegisterIn"] = "Изначально выбранное место этим ТС было закрыто, других свободных мест на данный момент нет"; //, с броней ничего не делать. Будет ждать свободных, не дождется - умрет сама.
                                                        return View("RegisterIn");
                                                    }
                                                }
                                                else
                                                {
                                                    place autoplace = mp.place.Where(x => x.Status == "Free").OrderBy(x => x.tariffonplace.PriceForHourWithoutAbonement).FirstOrDefault();
                                                    if (autoplace != null)
                                                    {
                                                        nvis.RegisterIn((long)autoplace.id_location_place, searchts.id_ts, Date, vp.id_vis_param);
                                                        p.ChangeStatus("Occupied", (long)autoplace.id_location_place, 0);
                                                    }
                                                    else
                                                    {
                                                        ViewData["ExceptionRegisterIn"] = "Свободных мест нет!";
                                                        return View("RegisterIn");
                                                    }
                                                    //Автоматический расчет мест";
                                                }
                                            }
                                            else
                                            {
                                                ViewData["ExceptionRegisterIn"] = "Возможно, Вы ошиблись при вводе регистрационного номера, т.к. указанное вами ТС уже находится на парковке.";
                                                return View("RegisterIn");
                                            }
                        }
                        else
                        {
                            ViewData["ExceptionRegisterIn"] = "TC не найдено в бд";
                            return View("RegisterIn");
                        }

                    }
                    else
                    {
                        ViewData["ExceptionRegisterIn"] = "Осутствуют параметры въезда, активной записи не найдено";
                        return View("RegisterIn");
                    }







                    return RedirectToAction("Index", new { Controller = "Home"});
                }
                else
                {
                    return View("RegisterIn");
                }
            

        }


        public ActionResult RegisterOut()
        {
            if (User.Identity.IsAuthenticated & User.IsInRole("Security"))
            {
                ViewData["ActiveVisit"] = mp.visit.Where(x => x.DateOut == "dateout").ToList();
                return View();
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterOut(ts numb)
        {
            ViewData["ActiveVisit"] = mp.visit.Where(x => x.DateOut == "dateout").ToList();
            if (ModelState.IsValidField("Number"))
            {
                ts searchts = mp.ts.Where(x => x.Number == numb.Number & x.Status == "True").FirstOrDefault();
                if (searchts != null)
                {
                    usingtariffonabonementforvisit expiredabonement = new usingtariffonabonementforvisit();
                    expiredabonement.ExpiredAbonement(searchts.Login);
                    visit vis = mp.visit.Where(x => x.id_ts == searchts.id_ts & x.DateOut == "dateout").FirstOrDefault();
                    if (vis != null)
                    {
                        Int64 id_abonement_u = 0;
                        bool dual = false;
                        tariffonplace top = mp.tariffonplace.Where(x => x.id_tariff_on_place == vis.place.id_alternative_tariff_on_place).FirstOrDefault();
                        decimal PriceForPlace = 0;
                        usingtariffonabonementforvisit abontar = mp.usingtariffonabonementforvisit.Where(x => x.Login == searchts.Login & x.Status == "Active").FirstOrDefault();

                        if (abontar != null)
                        {
                            PriceForPlace = top.PriceForHourWithAbonement;
                        }
                        else
                        {
                            var searchab = mp.usingtariffonabonementforvisit.Where(x => x.Status == "Not active" & x.Login == searchts.Login).ToList();
                            
                            int count = searchab.Count();
                            int i = 0;
                            foreach (var s1 in searchab)
                            {
                                DateTime max = Convert.ToDateTime(s1.DateOutFromActivity);
                                foreach (var maxdate in searchab)
                                {
                                    if (i < count)
                                    {
                                        if (max >= Convert.ToDateTime(maxdate.DateConnection))
                                        {
                                            max = Convert.ToDateTime(maxdate.DateConnection);
                                            if (max > Convert.ToDateTime(vis.DateIn))
                                            {
                                                id_abonement_u = maxdate.id_abonement;
                                            }
                                        }
                                        i++;
                                    }
                                    else { break; }
                                }
                                break;
                            }
                            if (id_abonement_u != 0)
                            {  
                                //Осталось учесть, если абонемент который на время сдох во время стоянки.
                            //Тогда поделить на 2 части. Первая по абонементской цене до этой даты, вторая по обычной, после выхода абонемента из строя
                            //Делаю в этой ветке, т.к. автомобилист уже мог подключить новый абонемент, а эта ветка происходит если нового
                            //не было найдено
                            //Причем это пригодится только для первой попытки выезда.
                            dual = true;
                            }
                            PriceForPlace = top.PriceForHourWithoutAbonement;
                        }

                        string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                        decimal price = 0;
                        usr ur = mp.usr.Where(x => x.Login == searchts.Login).FirstOrDefault();
                        balance bl = new balance();
                        place p = new place();

                        int y = 0;
                        if (vis.FirstAttemptGoOut == "dateout2")
                        {
                            //В минуты время простоя.
                            long span = 0;
                            DateTime timein = Convert.ToDateTime(vis.DateIn).AddMinutes(vis.visitparameters.FirstFreeTimeInMinutes);
                            if (timein < Convert.ToDateTime(Date))
                            {
                                if (dual == false)
                                {
                                    span = Convert.ToDateTime(Date).Ticks - timein.Ticks;

                                    //наличие абонемента играет роль
                                    decimal priceinmin = (decimal)(PriceForPlace) / 60;
                                    price = (decimal)TimeSpan.FromTicks(span).TotalMinutes * priceinmin;
                                    ur.Now_Balance = ur.Now_Balance - price;
                                    mp.Entry(ur).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, "Register Out. First Attempt.", Date);
                                }
                                else
                                {
                                    usingtariffonabonementforvisit uab = mp.usingtariffonabonementforvisit.Where(x => x.id_abonement == id_abonement_u).FirstOrDefault();
                            
                                    long span2 = 0;
                                    span = Convert.ToDateTime(uab.DateOutFromActivity).Ticks - timein.Ticks;
                                    span2 = Convert.ToDateTime(Date).Ticks - Convert.ToDateTime(uab.DateOutFromActivity).Ticks;
                                    decimal priceinmin1 = (decimal)(top.PriceForHourWithoutAbonement) / 60;
                                    decimal priceinmin2 = (decimal)(top.PriceForHourWithAbonement) / 60;
                                    price = ((decimal)TimeSpan.FromTicks(span).TotalMinutes * priceinmin1) + ((decimal)TimeSpan.FromTicks(span2).TotalMinutes * priceinmin2);
                                    ur.Now_Balance = ur.Now_Balance - price;
                                    mp.Entry(ur).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, "Register Out. First Attempt. With a both price, because your abonement was disabled during idle time", Date);
                                }
                            }
                            MyParkingEntities updout = new MyParkingEntities();
                            vis.FirstAttemptGoOut = Date;
                            mp.Entry(vis).State = EntityState.Modified;
                            mp.SaveChanges();
                            y = 1;
                        }
                        if (y == 0)
                        {
                            if (vis.FirstAttemptGoOut != "dateout2" & vis.NextAttemptGoOut == "dateout3")
                            {
                                long span = Convert.ToDateTime(Date).Ticks - Convert.ToDateTime(vis.FirstAttemptGoOut).Ticks;
                                decimal minutes = (decimal)TimeSpan.FromTicks(span).TotalMinutes - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans;

                                if (minutes > 0)
                                {
                                    price = Math.Abs(minutes) * ((decimal)PriceForPlace / 60);
                                    ur.Now_Balance = ur.Now_Balance - price;
                                    mp.Entry(ur).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, "Register Out. Second Attempt", Date);

                                }
                                y = 1;
                                MyParkingEntities updout = new MyParkingEntities();
                                vis.NextAttemptGoOut = Date;
                                mp.Entry(vis).State = EntityState.Modified;
                                mp.SaveChanges();
                            }

                        }
                        if (y == 0)
                        {
                            if (vis.NextAttemptGoOut != "dateout3")
                            {
                                //Сколько прошло от той попытки.
                                long span = Convert.ToDateTime(Date).Ticks - Convert.ToDateTime(vis.NextAttemptGoOut).Ticks;
                                decimal minutesnow = (decimal)TimeSpan.FromTicks(span).TotalMinutes;
                                //
                                //Для дальнейшего определения использован ли параметр бесплатного времени на покрытие брони.
                                long span2 = Convert.ToDateTime(vis.NextAttemptGoOut).Ticks - Convert.ToDateTime(vis.FirstAttemptGoOut).Ticks;
                                decimal minutes = (decimal)TimeSpan.FromTicks(span2).TotalMinutes;
                                //decimal minutes2 = (decimal)TimeSpan.FromTicks(span2).TotalMinutes - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans;
                                if (minutes - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans >= 0)
                                {
                                    price = Math.Abs(minutesnow) * ((decimal)PriceForPlace / 60);
                                    ur.Now_Balance = ur.Now_Balance - price;
                                    mp.Entry(ur).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, "Register Out. Next Attempt.", Date);
                                    //y = 1;
                                    MyParkingEntities updout = new MyParkingEntities();
                                    vis.NextAttemptGoOut = Date;
                                    mp.Entry(vis).State = EntityState.Modified;
                                    mp.SaveChanges();
                                }
                                else if (minutes - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans < 0)
                                {
                                    if ((minutes + minutesnow - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans) < 0)
                                    {
                                        MyParkingEntities updout = new MyParkingEntities();
                                        vis.NextAttemptGoOut = Date;
                                        mp.Entry(vis).State = EntityState.Modified;
                                        mp.SaveChanges();
                                    }
                                    else if ((minutes + minutesnow - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans) >= 0)
                                    {
                                        price = Math.Abs((minutes + minutesnow - (decimal)vis.visitparameters.FirstFreeTimeOnChangeBalans)) * ((decimal)PriceForPlace / 60);
                                        ur.Now_Balance = ur.Now_Balance - price;
                                        mp.Entry(ur).State = EntityState.Modified;
                                        mp.SaveChanges();
                                        bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, "Register Out. Next Attempt.", Date);
                                        //y = 1;
                                        MyParkingEntities updout = new MyParkingEntities();
                                        vis.NextAttemptGoOut = Date;
                                        mp.Entry(vis).State = EntityState.Modified;
                                        mp.SaveChanges();
                                    }
                                }
                            }
                        }
                        if (searchts.usr.Now_Balance >= 0)
                        {
                            //Если абонемент на кол-во посещений, то прибавить единицу и проверить не истек ли и если да, то сразу здесь же закрыть.
                            vis.DateOut = Date;
                            mp.Entry(vis).State = EntityState.Modified;
                            mp.SaveChanges();
                            place fp = new place();
                            fp.FreePlace(vis.id_location_place);
                            //разделить эти изменения по методам возникает ошибка.
                          // usingtariffonabonementforvisit abontar2 = mp.usingtariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == abontar.Name_tariff_on_abonement).FirstOrDefault();

                            //MyParkingEntities forabonement = new MyParkingEntities();
                            if (abontar != null)
                            {
                                abontar.NumOfVisitsMadeWithUsingThisTariff = abontar.NumOfVisitsMadeWithUsingThisTariff + 1;
                                mp.Entry(abontar).State = EntityState.Modified;
                               mp.SaveChanges();
                                usingtariffonabonementforvisit afterupdabontar = mp.usingtariffonabonementforvisit.Where(x => x.Login == searchts.Login & x.Status == "Active").FirstOrDefault();

                                if (abontar.tariffonabonementforvisit.Max_Num_visits_in_this_tariff != null)
                                {
                                    if (abontar.tariffonabonementforvisit.Max_Num_visits_in_this_tariff == afterupdabontar.NumOfVisitsMadeWithUsingThisTariff)
                                    {
                                        abontar.Status = "Expired";
                                        abontar.DateOutFromActivity = Date;
                                        mp.Entry(abontar).State = EntityState.Modified;
                                        mp.SaveChanges();
                                    }
                                }
                            }
                        }
                        else
                        {
                            ViewData["ExceptionRegisterOut"] = "Баланс отрицателен, полный выезд невозможен";
                            return View("RegisterOut");
                        }



                    }
                    else
                    {
                        ViewData["ExceptionRegisterOut"] = "Возможно, Вы ошиблись при вводе регистрационного номера, т.к. указанное вами ТС на парковке не найдено";
                        return View("RegisterOut");
                    }
                }
                else
                {
                    ViewData["ExceptionRegisterOut"] = "Возможно, Вы ошиблись при вводе регистрационного номера, т.к. указанное вами TC не найдено в бд";
                    return View("RegisterOut");
                }
                return RedirectToAction("Index", new { Controller = "Home"});
            }
            else
            {
                return View("RegisterOut");
            }
        }

    }
}
