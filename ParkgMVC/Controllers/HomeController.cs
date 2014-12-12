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

namespace ParkgMVC.Controllers
{
    public class HomeController : Controller
    {
        //RecordsVISITS recordsVISITS = new RecordsVISITS();
        //UsersTS UserTS = new UsersTS();
        MyParkingEntities mp = new MyParkingEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ZonesLevelsPlaces(string Value)
        {
            //Вы были перенаправлены на просмотр информации, поскольку формировать бронь без принятия соглашения запрещено правилами системы!
            if (Value != "RESERVATION")
            {
                ViewData["Reservation"] = "";
                return View(mp.parkingzone.ToList());
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    string Log = User.Identity.Name.ToString();
                    string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                    reservation activeres = mp.reservation.Where(x => x.Login == Log & x.Status == "Active").FirstOrDefault();
                    if (activeres == null)
                    {
                        reservation formedres = mp.reservation.Where(x => x.Login == Log & x.Status == "Formed").FirstOrDefault();
                        if (formedres != null)
                        {
                            ViewData["Reservation"] = "RESERVATION";
                            ViewData["ReservationPlace"] = "";
                            return View(mp.parkingzone.ToList());
                        }
                        else
                        {
                            ViewData["Reservation"] = "";
                            ViewData["ReservationPlace"] = "Вы были перенаправлены на просмотр информации, поскольку формировать бронь без принятия соглашения запрещено правилами системы!";
                            return View(mp.parkingzone.ToList());
                        }
                    }
                    else if (activeres != null)
                    {
                        ViewData["Reservation"] = "";
                        ViewData["ReservationPlace"] = "Вы были перенаправлены на просмотр информации, поскольку Вы уже имеете активное забронированное месторасположение!";
                        return View(mp.parkingzone.ToList());
                    }
                    return View(mp.parkingzone.ToList());
                }
                else
                {
                    return RedirectToAction("LogOn", new { Controller = "Account" });
                }


            }
        }



        public ActionResult Levels(Int32? Parking_zone)
        {
            if (Parking_zone != null)
            {
                levelzone lz = mp.levelzone.Where(x => x.Parking_zone == Parking_zone).FirstOrDefault();
                if (lz != null)
                {
                    ViewData["Reservation"] = "";
                    ViewData["Zone"] = "Зона №" + Convert.ToString(Parking_zone);
                    return View(mp.levelzone.Where(x => x.Parking_zone == Parking_zone));
                }
                else
                {
                    return RedirectToAction("ZonesLevelsPlaces");
                }
            }
            else
            {
                return RedirectToAction("ZonesLevelsPlaces");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Levels", MatchFormValue = "Select_zone")]
        public ActionResult Continue_Level(Int32 Parking_zone, string Value)
        {
            //Вернуть выборку по полученной зоне и значение ViewData
            ViewData["Reservation"] = Value;
            ViewData["Zone"] = "Зона №" + Convert.ToString(Parking_zone);

            //Вернуть выборку по полученной зоне и значение ViewData
            return View(mp.levelzone.Where(x => x.Parking_zone == Parking_zone));
        }


        public ActionResult Places()
        {
            return RedirectToAction("ZonesLevelsPlaces");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "Select_level")]
        public ActionResult Continue_place(Int32 id_location_level, string Value)
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            reservation find = new reservation();
            find.FindOnExpired("");
            levelzone levz = mp.levelzone.Where(x => x.id_location_level == id_location_level).FirstOrDefault();
            Int32 zone = levz.Parking_zone;
            Int32 level = levz.Level;
            string type_level = levz.TypeLevel;
            ViewData["Zone-Level"] = "Зона №" + Convert.ToString(zone) + " ; Уровень:" + Convert.ToString(level) + " ; Тип уровня: " + type_level;
            if (Value == "RESERVATION")
            {
                ViewData["Reservation"] = Value;
                return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x=>x.NumberPlace));
            }
            else
            {
                ViewData["Reservation"] = "";
                return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status != "Was replaced").OrderBy(x=>x.NumberPlace));
            }
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "ConnectReservation")]
        public ActionResult ConnectReservation(Int32 ChoosePlace, Int32 id_location_level, FormCollection form)
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            reservation find = new reservation();
            find.FindOnExpired("");
            levelzone levz = mp.levelzone.Where(x => x.id_location_level == id_location_level).FirstOrDefault();
            Int32 zone = levz.Parking_zone;
            Int32 level = levz.Level;
            string type_level = levz.TypeLevel;
            ViewData["Zone-Level"] = "Зона №" + Convert.ToString(zone) + " ; Уровень:" + Convert.ToString(level) + " ; Тип уровня: " + type_level;
            ViewData["Reservation"] = "RESERVATION";

            if (User.Identity.IsAuthenticated)
            {
                string Log = User.Identity.Name.ToString();
                string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");

                reservation activeres = mp.reservation.Where(x => x.Login == Log & x.Status == "Active").FirstOrDefault();
                if (activeres == null)
                {

                    reservation formedres = mp.reservation.Where(x => x.Login == Log & x.Status == "Formed").FirstOrDefault();
                    if (formedres != null)
                    {
                                ts exist = mp.ts.Where(x => x.Login == Log & x.Status == "True").FirstOrDefault();
                                if (exist != null)
                                {

                                    visit vis = mp.visit.Where(x => x.id_ts == exist.id_ts & x.DateOut == "dateout").FirstOrDefault();
                                    if (vis == null)
                                    {
                        usr us = mp.usr.Where(x => x.Login == Log).FirstOrDefault();
                        place free = mp.place.Where(x => x.id_location_place == ChoosePlace).FirstOrDefault();
                        if (us != null & free.Status == "Free")
                        {
                            if (us.Now_Balance >= 0)
                            {


                                    reservation_tariff tar = mp.reservation_tariff.Where(x => x.Status == "available" & x.id_Reservation_Tariff == formedres.id_Reservation_Tariff).FirstOrDefault();
                                    if (tar != null)
                                    {

                                        //tariffOnplace p = mp.tariff_on_place.where(x=>x.id_tariff_on_place == tariff & x.Status == "Active")
                                        //if p != null
                                        formedres.id_location_place = ChoosePlace;
                                        formedres.id_alternative_location_place = ChoosePlace;
                                        formedres.Status = "Active";
                                        formedres.Description = "Reservation connect";
                                        formedres.DateConnection = Date;
                                        DateTime mydate = Convert.ToDateTime(Date).AddHours(tar.ValidityPeriodFromTheTimeOfActivationInHour);//Согласно активному тарифу
                                        formedres.ApproximatelyDateOutFromActivity = Convert.ToString(mydate);
                                        mp.Entry(formedres).State = EntityState.Modified;
                                        mp.SaveChanges();
                                        place newplace = new place();
                                        newplace.ChangeStatus("In waiting visit", (long)formedres.id_location_place,0);

                                        //else p==null return сообщение что тариф для места изменился, отсылаю через вбю дата актуальные тарифы и места с актуальными тарифами
                                        //Подумать стоит ли делать это(комментированный примерный код) в ResController при подключении брони
                                    }
                                    else
                                    {
                                        //У формируемой брони изменился тариф.
                                        formedres.id_location_place = ChoosePlace;
                                        formedres.id_alternative_location_place = ChoosePlace;
                                        mp.Entry(formedres).State = EntityState.Modified;
                                        mp.SaveChanges();
                                        return RedirectToAction("Agreement", new { Controller = "Res" });
                                    }



                            }
                            else if (us.Now_Balance < 0)
                            {
                                ViewData["ReservationPlace"] = "Активирование брони с отрицательным балансом запрещено. Формируемая вами бронь сохранена, вы сможете подключить ее после пополнения баланса!";
                                formedres.id_location_place = ChoosePlace;
                                formedres.id_alternative_location_place = ChoosePlace;
                                mp.Entry(formedres).State = EntityState.Modified;
                                mp.SaveChanges();

                                return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                            }

                        }
                        else if (free.Status != "Free")
                        {
                            formedres.id_location_place = ChoosePlace;
                            formedres.id_alternative_location_place = ChoosePlace;
                            mp.Entry(formedres).State = EntityState.Modified;
                            mp.SaveChanges();
                            if (free.Status == "Occupied" || free.Status == "In waiting visit")
                            {
                                ViewData["ReservationPlace"] = "Место №" + free.NumberPlace + " занято!";
                            }
                            else
                            {
                                ViewData["ReservationPlace"] = "Место недоступно!";
                            }
                            return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x=>x.NumberPlace).ToList());
                        }
            }
                else
                {
                    ViewData["ReservationPlace"] = "Формиремая бронь сохранена, но Вы не можете активировать бронирование, пока одно из ваших ТС находится у нас на парковке!";
                                        formedres.id_location_place = ChoosePlace;
                        formedres.id_alternative_location_place = ChoosePlace;
                        mp.Entry(formedres).State = EntityState.Modified;
                        mp.SaveChanges();
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                   
                                }
                    }
                    else
                    {
                        ViewData["ReservationPlace"] = "Активирование брони без наличия ТС запрещено. Формируемая вами бронь сохранена, вы сможете подключить ее после добавления вашего ТС!";
                        formedres.id_location_place = ChoosePlace;
                        formedres.id_alternative_location_place = ChoosePlace;
                        mp.Entry(formedres).State = EntityState.Modified;
                        mp.SaveChanges();
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                    }
                    }
                    else
                    {
                        //МОЖНО СДЕЛАТЬ ПЕРЕНАПРАВЛЕНИЕ СРАЗУ НА Reservation в ResController с этим же самым сообщением. Или на принятие соглашения.
                        ViewData["ReservationPlace"] = "Система не нашла формирующейся заявки! Возможно, она была удалена вами.";
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                    }





                }

                else if (activeres != null)
                {
                    ViewData["ReservationPlace"] = "Вы уже имеете активное забронированное месторасположение!";
                    return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                }
                return RedirectToAction("Reservation", new { Controller = "Res" });
                //  return View(mp.place.Where(x => x.id_location_level == id_location_level).ToList());
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
        }




        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "SaveReservation")]
        public ActionResult SaveReservation(Int32 ChoosePlace, Int32 id_location_level, FormCollection form)
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            levelzone levz = mp.levelzone.Where(x => x.id_location_level == id_location_level).FirstOrDefault();
            Int32 zone = levz.Parking_zone;
            Int32 level = levz.Level;
            string type_level = levz.TypeLevel;
            if (User.Identity.IsAuthenticated)
            {
                string Log = User.Identity.Name.ToString();
                string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");

                reservation activeres = mp.reservation.Where(x => x.Login == Log & x.Status == "Active").FirstOrDefault();
                if (activeres == null)
                {
                    reservation formedres = mp.reservation.Where(x => x.Login == Log & x.Status == "Formed").FirstOrDefault();
                    if (formedres != null)
                    {
                        reservation_tariff tar = mp.reservation_tariff.Where(x => x.Status == "available" & x.id_Reservation_Tariff == formedres.id_Reservation_Tariff).FirstOrDefault();
                        if (tar != null)
                        {
                            usr us = mp.usr.Where(x => x.Login == Log).FirstOrDefault();
                            if (us != null)
                            {
                                formedres.id_location_place = ChoosePlace;
                                formedres.id_alternative_location_place = ChoosePlace;
                                formedres.Status = "Formed";
                                formedres.Description = "Reservation formed and wait connect";
                                mp.Entry(formedres).State = EntityState.Modified;
                                mp.SaveChanges();
                            }
                        }
                        else
                        {
                            formedres.id_location_place = ChoosePlace;
                            formedres.id_alternative_location_place = ChoosePlace;
                            mp.Entry(formedres).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                    }
                    else
                    {
                        ViewData["ReservationPlace"] = "Система не нашла формирующейся заявки! Возможно, она была удалена вами.";
                        ViewData["Reservation"] = "RESERVATION";
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                    }
                }
                else if (activeres != null)
                {
                    ViewData["ReservationPlace"] = "Вы уже имеете активное забронированное месторасположение!";
                    ViewData["Reservation"] = "RESERVATION";
                    return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").OrderBy(x => x.NumberPlace).ToList());
                }
                ViewData["ReservationTariff"] = mp.reservation_tariff.Where(x => x.Status == "available").ToList();
                return RedirectToAction("Reservation", new { Controller = "Res" });
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
        }
        //Не совсем работает так.Когда перехожу с дешевого на дорогой тариф, в альтернативный записыватся дорогой а не дешевый первоначальный
        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "Stop_work")]
        public ActionResult Stop_work(Int32 id_location_level, Int32 id_loc_pl, FormCollection form)
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");

            reservation expired = new reservation();
            expired.FindOnExpired("");

            place not_working_place = mp.place.Where(x => x.id_location_place == id_loc_pl).FirstOrDefault();
            place change = new place();

            if (not_working_place.Status == "In waiting visit")
            {
                change.ChangeStatus("Not working", id_loc_pl, 0);
                reservation res = mp.reservation.Where(x => x.Status == "Active" & (x.id_location_place == id_loc_pl || x.id_alternative_location_place == id_loc_pl)).FirstOrDefault();
                if (res != null)
                {
                    place findmax = mp.place.Where(x => (x.tariffonplace.PriceForHourWithoutAbonement >= res.place.tariffonplace.PriceForHourWithoutAbonement) & x.Status == "Free").OrderBy(x => x.tariffonplace.PriceForHourWithAbonement).FirstOrDefault();
                    if (findmax != null)
                    {
                        change.ChangeStatus("In waiting visit", (long)findmax.id_location_place, (int)res.place.id_tariff_on_place);
                        res.id_alternative_location_place = (int)findmax.id_location_place;
                        mp.Entry(res).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                    else if (findmax == null)
                    {
                        place findmin = mp.place.Where(x => (x.tariffonplace.PriceForHourWithoutAbonement < res.place.tariffonplace.PriceForHourWithoutAbonement) & x.Status == "Free").OrderByDescending(x => x.tariffonplace.PriceForHourWithAbonement).FirstOrDefault();
                        if (findmin != null)
                        {
                            change.ChangeStatus("In waiting visit", (long)findmin.id_location_place, 0);
                            res.id_alternative_location_place = (int)findmin.id_location_place;
                            mp.Entry(res).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                        else
                        {
                            res.id_alternative_location_place = null;
                            mp.Entry(res).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                    }
                }
            }
            else { change.ChangeStatus("Not working", id_loc_pl, 0); }

            //& (x.Status == "Free" || x.Status == "Not working" || x.Status == "In waiting visit")
            ViewData["Reservation"] = "";
            ViewData["Zone-Level"] = "Зона №" + Convert.ToString(not_working_place.levelzone.Parking_zone) + " ; Уровень:" + Convert.ToString(not_working_place.levelzone.Level) + " ; Тип уровня: " + not_working_place.levelzone.TypeLevel;
            ViewData["ReservationPlace"] = "Место №" + not_working_place.NumberPlace + " было успешно переведено в неактивное состояние.";
            return View(mp.place.Where(x => x.id_location_level == id_location_level & (x.Status == "Free" || x.Status == "Not working" || x.Status == "In waiting visit")).OrderBy(x => x.NumberPlace).ToList());
            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "Start_work")]
        public ActionResult Start_work(Int32 id_location_level, Int32 id_loc_pl, FormCollection form)
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            reservation expired = new reservation();
            expired.FindOnExpired("");
            place not_working_place = mp.place.Where(x => x.id_location_place == id_loc_pl).FirstOrDefault();
            place change = new place();
            
            reservation res = mp.reservation.Where(x => x.Status == "Active" & (x.id_location_place == id_loc_pl)).FirstOrDefault();
            if (res != null)
            {
                if (res.id_alternative_location_place != null)
                {
                    change.FreePlace((long)res.id_alternative_location_place);
                }
                res.id_alternative_location_place = id_loc_pl;
                mp.Entry(res).State = EntityState.Modified;
                mp.SaveChanges();
                change.ChangeStatus("In waiting visit", id_loc_pl, 0); 
            }
            else
            {
                change.FreePlace((long)id_loc_pl);
            }
            ViewData["Reservation"] = "";
            ViewData["Zone-Level"] = "Зона №" + Convert.ToString(not_working_place.levelzone.Parking_zone) + " ; Уровень:" + Convert.ToString(not_working_place.levelzone.Level) + " ; Тип уровня: " + not_working_place.levelzone.TypeLevel;           
            ViewData["ReservationPlace"] = "Место №" + not_working_place.NumberPlace + " было успешно переведено в активное состояние.";
            return View(mp.place.Where(x => x.id_location_level == id_location_level & (x.Status == "Free" || x.Status == "Not working" || x.Status == "In waiting visit")).OrderBy(x => x.NumberPlace).ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Place_tariff", MatchFormValue = "Change_tariff_for_this_place")]
        public ActionResult Change_tariff_for_this_place(Int32 id_location_level, Int32 id_loc_pl, FormCollection form)
        {
            
            place not_working_place = mp.place.Where(x => x.id_location_place == id_loc_pl).FirstOrDefault();
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active" & x.id_tariff_on_place != not_working_place.id_tariff_on_place);
            ViewData["Place_tariff"] = "Выбор нового тарифа для месторасположения";
            //ViewData["Place_tariff_err_message"] = "Тариф для места №" + not_working_place.NumberPlace + " был успешно сменен.";
            return View(not_working_place);
        }


        public ActionResult Place_tariff(Int32? objp, Int32? txt)
        {
            try
            {
                if (objp != null & txt != null)
                {
                    place not_working_plac = mp.place.Where(x => x.id_location_place == objp).FirstOrDefault();
                    ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
                    if (txt == 1)
                    {
                        ViewData["Place_tariff_err_message"] = "Выбранный Вами тариф перестал быть активным.";
                    }

                    ViewData["Place_tariff"] = "Выбор нового тарифа для месторасположения";
                    return View(not_working_plac);
                }
                else
                {
                    return RedirectToAction("ZonesLevelsPlaces");
                }

            }
            catch {
                return RedirectToAction("ZonesLevelsPlaces");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "Choose_tariff_on_place")]
        public ActionResult Choose_tariff_on_place(Int32 not_working_place, Int32 choose_tariff_on_place, FormCollection form)
        {//Может вывести в качестве информации текущий тариф??
            reservation expired = new reservation();
            expired.FindOnExpired("");
            ViewData["Reservation"] = "";
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            place not_working_plac = mp.place.Where(x => x.id_location_place == not_working_place).FirstOrDefault();

            tariffonplace choosetop = mp.tariffonplace.Where(x => x.id_tariff_on_place == choose_tariff_on_place & x.Status == "Active").FirstOrDefault();
            if (choosetop != null)
            {
                
                    reservation searchactiveres = mp.reservation.Where(x => x.id_location_place == not_working_place & x.Status == "Active").FirstOrDefault();
                    if (searchactiveres != null)
                    {
                        ViewData["ReservationPlace"] = "Вы не можете изменить тариф для этого места, пока в этом расположении нуждается один из участников системы";
                    }
                    else
                    {
                        place thplace = mp.place.Where(x => x.id_location_level == not_working_plac.id_location_level & x.NumberPlace == not_working_plac.NumberPlace & x.Status == "Was replaced" & x.id_tariff_on_place == choose_tariff_on_place).FirstOrDefault();
                        if (thplace == null)
                        {
                            place place_new = new place();
                            place_new.id_location_level = not_working_plac.id_location_level;
                            place_new.id_tariff_on_place = choose_tariff_on_place;
                            place_new.Status = not_working_plac.Status;
                            place_new.NumberPlace = not_working_plac.NumberPlace;
                            place_new.id_alternative_tariff_on_place = choose_tariff_on_place;
                            mp.place.Add(place_new);
                            mp.SaveChanges();

                            not_working_plac.Status = "Was replaced";
                            mp.Entry(not_working_plac).State = EntityState.Modified;
                            mp.SaveChanges();
                            ViewData["ReservationPlace"] = "Тариф для места №" + not_working_plac.NumberPlace + " был успешно сменен.";
                        }
                        else
                        {
                            thplace.Status = not_working_plac.Status;
                            mp.Entry(thplace).State = EntityState.Modified;
                            mp.SaveChanges();

                            not_working_plac.Status = "Was replaced";
                            mp.Entry(not_working_plac).State = EntityState.Modified;
                            mp.SaveChanges();
                            ViewData["ReservationPlace"] = "Тариф для места №" + not_working_plac.NumberPlace + " был успешно сменен.";
                        }
                    
                }

                    //return View(mp.place.Where(x => x.id_location_level == not_working_plac.id_location_level & x.Status != "Was replaced").OrderBy(x=>x.NumberPlace).ToList());

                ViewData["Zone-Level"] = "Зона №" + Convert.ToString(not_working_plac.levelzone.Parking_zone) + " ; Уровень:" + Convert.ToString(not_working_plac.levelzone.Level) + " ; Тип уровня: " + not_working_plac.levelzone.TypeLevel;
                return View(mp.place.Where(x => x.id_location_level == not_working_plac.id_location_level & (x.Status == "Free" || x.Status == "Not working" || x.Status == "In waiting visit")).OrderBy(x=>x.NumberPlace).ToList());
            }
            else
            {
                return RedirectToAction("Place_tariff", new { objp = not_working_place, txt = 1 });
            }
        }

        

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_a_p", MatchFormValue = "Edit_amount_place")]
        public ActionResult Edit_amount_place(Int32 id_location_level)
        {
            levelzone edit_place_for_this_level = mp.levelzone.Where(x => x.id_location_level == id_location_level).FirstOrDefault();
            return View(edit_place_for_this_level);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_a_p", MatchFormValue = "Change_level")]
        public ActionResult Change_level(Int32 id_location_level, Int32 Parking_zone, string AmPlace, string AdPlace, FormCollection form)
        {
            //В случае успеха вернуть
           // return RedirectToAction("Levels", new { Parking_zone = Parking_zone });

            ViewData["EditLevel"] = AmPlace + " " + AdPlace;





            levelzone edit_place_for_this_level = mp.levelzone.Where(x => x.id_location_level == id_location_level).FirstOrDefault();
            return View(edit_place_for_this_level);
        }
        /*

        //==========================================

        public ActionResult EL(string Parking_zone, string Levels, string Address)
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EL(string Parking_zone, parkingzones visits)
        {
            try
            {
                if (lz.EditLev(Parking_zone, visits))
                    return RedirectToAction("ZonesLevelsPlaces");
                else
                    return View("EditLev");
            }
            catch
            {
                return View("EditLev");
            }
        }

        //============================================


        public ActionResult VISIT()
        {
            string Log = User.Identity.Name.ToString();
            return View(recordsVISITS.VISIT(Log));
        }

        public ActionResult CreateVISIT()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateVISIT(visit vis)
        {
            try
            {
                if (recordsVISITS.RegInTS(vis))
                    return RedirectToAction("VISIT");
                else
                    return View("CreateVISIT");
            }
            catch
            {
                return View("CreateVISIT");
            }
        }
        Places p = new Places();
        public ActionResult EditAmountPlace(string id_location_level)
        {
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditAmountPlace(string id_location_level, places pl)
        {
            try
            {
                if (p.AmountPlace(id_location_level, pl))
                    return RedirectToAction("Levels");
                else
                    return View("EditLev");
            }
            catch
            {
                return View("EditLev");
            }
        }*/

    }
}
