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



        public ActionResult Levels()
        {
            return RedirectToAction("ZonesLevelsPlaces");
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
                return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free"));
            }
            else
            {
                ViewData["Reservation"] = "";
                return View(mp.place.Where(x => x.id_location_level == id_location_level));
            }
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Places", MatchFormValue = "ConnectReservation")]
        public ActionResult ConnectReservation(Int32 ChoosePlace, Int32 id_location_level, FormCollection form)
        {
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



                        usr us = mp.usr.Where(x => x.Login == Log).FirstOrDefault();
                        place free = mp.place.Where(x => x.id_location_place == ChoosePlace & x.Status == "Free").FirstOrDefault();
                        if (us != null & free != null)
                        {
                            if (us.Now_Balance >= 0)
                            {
                                ts exist = mp.ts.Where(x => x.Login == Log & x.Status == "True").FirstOrDefault();
                                if (exist != null)
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
                                        newplace.ChangeStatus("In waiting visit", (long)formedres.id_location_place);

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
                                else
                                {
                                    ViewData["ReservationPlace"] = "Активирование брони без наличия ТС запрещено. Формируемая вами бронь сохранена, вы сможете подключить ее после добавления вашего ТС!";
                                    formedres.id_location_place = ChoosePlace;
                                    formedres.id_alternative_location_place = ChoosePlace;
                                    mp.Entry(formedres).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                                }
                            }
                            else if (us.Now_Balance < 0)
                            {
                                ViewData["ReservationPlace"] = "Активирование брони с отрицательным балансом запрещено. Формируемая вами бронь сохранена, вы сможете подключить ее после пополнения баланса!";
                                formedres.id_location_place = ChoosePlace;
                                formedres.id_alternative_location_place = ChoosePlace;
                                mp.Entry(formedres).State = EntityState.Modified;
                                mp.SaveChanges();

                                return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                            }

                        }
                        else if (free == null)
                        {
                            ViewData["ReservationPlace"] = "Место №" + free.NumberPlace + " занято!";
                            return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                        }


                    }
                    else
                    {
                        ViewData["ReservationPlace"] = "Система не нашла формирующейся заявки! Возможно, она была удалена вами.";
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
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
                        return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                    }
                }
                else if (activeres != null)
                {
                    ViewData["ReservationPlace"] = "Вы уже имеете активное забронированное месторасположение!";
                    ViewData["Reservation"] = "RESERVATION";
                    return View(mp.place.Where(x => x.id_location_level == id_location_level & x.Status == "Free").ToList());
                }
                ViewData["ReservationTariff"] = mp.reservation_tariff.Where(x => x.Status == "available").ToList();
                return RedirectToAction("Reservation", new { Controller = "Res" });
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
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
