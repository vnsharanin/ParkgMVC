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
    public class TariffsController : Controller
    {
        //
        // GET: /Tariffs/
        MyParkingEntities mp = new MyParkingEntities();
        public ActionResult AllTariffs()
        {
            return View();
        }
        public ActionResult ReservationTariffs()
        {
            ViewData["ActiveTariff"] = mp.reservation_tariff.Where(x => x.Status == "available");
            //но если налл, создать еще одну вью дата которая вернет сообщение, что тарифов нет.
            ViewData["NotActiveTariffs"] = mp.reservation_tariff.Where(x => x.Status != "available");
            return View();
        }

        public ActionResult Create_reservation_tariff()
        {
            //
            return RedirectToAction("ReservationTariffs");
        }

        public ActionResult Edit_reservation_tariff()
        {
            //
            return RedirectToAction("ReservationTariffs");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_reservation_tariff", MatchFormValue = "New_reservation_tariff")]
        public ActionResult New_reservation_tariff()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_reservation_tariff", MatchFormValue = "Edit_reservation_tariff")]
        public ActionResult Edit_reservation_tariff(Int32 id_reservation_tariff)
        {
            reservation_tariff ch = mp.reservation_tariff.Where(x => x.id_Reservation_Tariff == id_reservation_tariff & x.Status =="available").FirstOrDefault();
            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return RedirectToAction("ReservationTariffs");
            }
        }

         [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_reservation_tariff", MatchFormValue = "Off_reservation_tariff")]
        public ActionResult Off_reservation_tariff(Int32 id_reservation_tariff)
        {
            reservation_tariff res = mp.reservation_tariff.Where(x => x.id_Reservation_Tariff == id_reservation_tariff).FirstOrDefault();
            if (res != null)
            {
                res.Status = "Not available";
                mp.Entry(res).State = EntityState.Modified;
                mp.SaveChanges();
            }
            return RedirectToAction("ReservationTariffs");
        }

         [AcceptVerbs(HttpVerbs.Post)]
         [MultiButton(MatchFormKey = "Edit_reservation_tariff", MatchFormValue = "Activate_reservation_tariff")]
         public ActionResult Activate_reservation_tariff(Int32 id_reservation_tariff)
         {
             reservation_tariff res = mp.reservation_tariff.Where(x => x.id_Reservation_Tariff == id_reservation_tariff).FirstOrDefault();
             if (res != null)
             {
                 reservation_tariff act = mp.reservation_tariff.Where(x => x.Status == "available").FirstOrDefault();
                 if (act != null)
                 {
                     act.Status = "Not available";
                     mp.Entry(act).State = EntityState.Modified;
                     mp.SaveChanges();
                 }
                     res.Status = "available";
                     mp.Entry(res).State = EntityState.Modified;
                     mp.SaveChanges();
                 
             }
             return RedirectToAction("ReservationTariffs");
         }


        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_reservation_tariff", MatchFormValue = "Save_new_reservation_tariff")]
        public ActionResult Save_reservation_tariff(reservation_tariff rt)
        {

            reservation_tariff old = mp.reservation_tariff.Where(x => x.PriceInRubForHourHightFreeTime == rt.PriceInRubForHourHightFreeTime & x.FirstFreeTimeInMinutes == rt.FirstFreeTimeInMinutes & x.ValidityPeriodFromTheTimeOfActivationInHour == rt.ValidityPeriodFromTheTimeOfActivationInHour).FirstOrDefault();
            if (old != null)
            {
                if (old.Status == rt.Status & rt.Status == "available")
                {
                    ViewData["ReservationTariff"] = "Такой тариф уже существует и он активен";
                }
                else if (old.Status == rt.Status & rt.Status == "Not available")
                {

                    reservation_tariff oldforchange = mp.reservation_tariff.Where(x => x.Status == "available").FirstOrDefault();
                    if (rt.Status == "available")
                    {
                        if (oldforchange != null)
                        {
                            oldforchange.Status = "Not available";
                            mp.Entry(oldforchange).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                    }

                    old.Status = rt.Status;
                    mp.Entry(old).State = EntityState.Modified;
                    mp.SaveChanges();
                }
            }
            else
            {
                reservation_tariff oldforchange = mp.reservation_tariff.Where(x => x.Status == "available").FirstOrDefault();
                if (rt.Status == "available")
                {
                    if (oldforchange != null)
                    {
                        oldforchange.Status = "Not available";
                        mp.Entry(oldforchange).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                }

                reservation_tariff rnew = new reservation_tariff();
                rnew.Status = rt.Status;
                rnew.ValidityPeriodFromTheTimeOfActivationInHour = rt.ValidityPeriodFromTheTimeOfActivationInHour;
                rnew.PriceInRubForHourHightFreeTime = rt.PriceInRubForHourHightFreeTime;
                rnew.FirstFreeTimeInMinutes = rt.FirstFreeTimeInMinutes;
                mp.reservation_tariff.Add(rnew);
                mp.SaveChanges();
            }

            return RedirectToAction("ReservationTariffs");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_reservation_tariff", MatchFormValue = "Change_reservation_tariff")]
        public ActionResult Change_reservation_tariff(reservation_tariff rt, Int32 id_reservation_tariff)
        {
            if (id_reservation_tariff != null)
            {
                reservation_tariff oldforchange = mp.reservation_tariff.Where(x => x.id_Reservation_Tariff == id_reservation_tariff).FirstOrDefault();
                reservation_tariff old = mp.reservation_tariff.Where(x => x.id_Reservation_Tariff == id_reservation_tariff & x.PriceInRubForHourHightFreeTime == rt.PriceInRubForHourHightFreeTime & x.FirstFreeTimeInMinutes == rt.FirstFreeTimeInMinutes & x.ValidityPeriodFromTheTimeOfActivationInHour == rt.ValidityPeriodFromTheTimeOfActivationInHour).FirstOrDefault();
                if (old != null)
                {
                        ViewData["ReservationTariff"] = "Изменений не произошло!";
                        return View(old);
                }
                else
                {
                    if (oldforchange.Status != "available")
                    {
                        ViewData["ReservationTariff"] = "Этот тариф больше неактивен, применить к нему измененения невозможно!";
                        return View(old);
                    }
                    else
                    {
                        //произвожу изменение статуса тарифа на not active в oldforchange
                        oldforchange.Status = "Not available";
                        mp.Entry(oldforchange).State = EntityState.Modified;
                        mp.SaveChanges();
                        reservation_tariff old2 = mp.reservation_tariff.Where(x =>x.PriceInRubForHourHightFreeTime == rt.PriceInRubForHourHightFreeTime & x.FirstFreeTimeInMinutes == rt.FirstFreeTimeInMinutes & x.ValidityPeriodFromTheTimeOfActivationInHour == rt.ValidityPeriodFromTheTimeOfActivationInHour).FirstOrDefault();
                        if (old2 != null)
                        {
                            if (old2.Status != "available")
                            {
                                old2.Status = "available";
                                mp.Entry(old2).State = EntityState.Modified;
                                mp.SaveChanges();
                            }
                        }
                        else
                        {
                            reservation_tariff rnew = new reservation_tariff();
                            rnew.Status = "available";
                            rnew.ValidityPeriodFromTheTimeOfActivationInHour = rt.ValidityPeriodFromTheTimeOfActivationInHour;
                            rnew.PriceInRubForHourHightFreeTime = rt.PriceInRubForHourHightFreeTime;
                            rnew.FirstFreeTimeInMinutes = rt.FirstFreeTimeInMinutes;
                            mp.reservation_tariff.Add(rnew);
                            mp.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("ReservationTariffs");
        }

        public ActionResult VisitParameters()
        {
            ViewData["ActiveParameters"] = mp.visitparameters.Where(x => x.Status == "Active");
            ViewData["NotActiveParameters"] = mp.visitparameters.Where(x => x.Status != "Active");
            return View();
        }

        public ActionResult Create_visit_parameters()
        {
            //
            return RedirectToAction("VisitParameters");
        }

        public ActionResult Edit_visit_parameters()
        {
            //
            return RedirectToAction("VisitParameters");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_visit_parameters", MatchFormValue = "Edit_visit_parameters")]
        public ActionResult Edit_visit_parameters(Int32 id_vis_param)
        {
            visitparameters ch = mp.visitparameters.Where(x => x.id_vis_param == id_vis_param & x.Status == "Active").FirstOrDefault();
            if (ch != null)
            {
                return View(ch);
            }
            else
            {
                return RedirectToAction("VisitParameters");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_visit_parameters", MatchFormValue = "New_visit_parameters")]
        public ActionResult New_visit_parameters()
        {
            return View();
        }





        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_visit_parameters", MatchFormValue = "Save_visit_parameters")]
        public ActionResult Save_visit_parameters(visitparameters vp)
        {
            //ViewData["VisitParameters"] = "";
            //Наверное зедсь будет лучше если найдется запись с такими же параметрами в Not active, а создаваемые такие же будут Active,
            //Тогда перевести просто ту запись в Active.
            visitparameters old = mp.visitparameters.Where(x => x.FirstFreeTimeInMinutes == vp.FirstFreeTimeInMinutes & x.FirstFreeTimeOnChangeBalans == vp.FirstFreeTimeOnChangeBalans).FirstOrDefault();
            if (old != null)
            {
                if (old.Status == vp.Status & vp.Status == "Active")
                {
                    ViewData["VisitParameters"] = "Такой тариф уже существует и он активен";
                    return View(vp);
                }
                else if (old.Status == vp.Status & vp.Status != "Active")
                {

                    visitparameters oldforchange = mp.visitparameters.Where(x => x.Status == "Active").FirstOrDefault();
                    if (vp.Status == "Active")
                    {
                        if (oldforchange != null)
                        {
                            oldforchange.Status = "Not active";
                            mp.Entry(oldforchange).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                    }

                    old.Status = vp.Status;
                    mp.Entry(old).State = EntityState.Modified;
                    mp.SaveChanges();
                }
            }
            else
            {
                visitparameters oldforchange = mp.visitparameters.Where(x => x.Status == "Active").FirstOrDefault();
                if (vp.Status == "Active")
                {
                    if (oldforchange != null)
                    {
                        oldforchange.Status = "Not active";
                        mp.Entry(oldforchange).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                }

                visitparameters vnew = new visitparameters();
                vnew.Status = vp.Status;
                vnew.FirstFreeTimeInMinutes = vp.FirstFreeTimeInMinutes;
                vnew.FirstFreeTimeOnChangeBalans = vp.FirstFreeTimeOnChangeBalans;
                mp.visitparameters.Add(vnew);
                mp.SaveChanges();
            }

            return RedirectToAction("VisitParameters");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_visit_parameters", MatchFormValue = "Change_visit_parameters")]
        public ActionResult Change_visit_parameters(visitparameters vp, Int32? id_vis_param)
        {
            if (id_vis_param != null)
            {
                visitparameters oldforchange = mp.visitparameters.Where(x => x.id_vis_param == id_vis_param).FirstOrDefault();
                if (oldforchange != null)
                {
                    visitparameters old = mp.visitparameters.Where(x => x.id_vis_param == id_vis_param & x.FirstFreeTimeInMinutes == vp.FirstFreeTimeInMinutes & x.FirstFreeTimeOnChangeBalans == vp.FirstFreeTimeOnChangeBalans).FirstOrDefault();
                    if (old != null)
                    {
                        ViewData["VisitParameters"] = "Изменений не произошло!";
                        return View(old);
                    }
                    else
                    {
                        if (oldforchange.Status != "Active")
                        {
                            ViewData["VisitParameters"] = "Этот набор больше неактивен, применить к нему измененения невозможно!";
                            return View(old);
                        }
                        else
                        {
                            //произвожу изменение статуса тарифа на not active в oldforchange
                            oldforchange.Status = "Not active";
                            mp.Entry(oldforchange).State = EntityState.Modified;
                            mp.SaveChanges();
                            visitparameters old2 = mp.visitparameters.Where(x => x.FirstFreeTimeInMinutes == vp.FirstFreeTimeInMinutes & x.FirstFreeTimeOnChangeBalans == vp.FirstFreeTimeOnChangeBalans).FirstOrDefault();
                            if (old2 != null)
                            {
                                if (old2.Status != "Active")
                                {
                                    old2.Status = "Active";
                                    mp.Entry(old2).State = EntityState.Modified;
                                    mp.SaveChanges();
                                }
                            }
                            else
                            {
                                visitparameters vnew = new visitparameters();
                                vnew.Status = "Active";
                                vnew.FirstFreeTimeInMinutes = vp.FirstFreeTimeInMinutes;
                                vnew.FirstFreeTimeOnChangeBalans = vp.FirstFreeTimeOnChangeBalans;
                                mp.visitparameters.Add(vnew);
                                mp.SaveChanges();
                            }
                        }

                    }
                }
                
            }return RedirectToAction("VisitParameters");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_visit_parameters", MatchFormValue = "Off_parameters")]
        public ActionResult Off_parameters(Int32 id_vis_param)
        {
            visitparameters vp = mp.visitparameters.Where(x => x.id_vis_param == id_vis_param & x.FirstFreeTimeInMinutes != 0 & x.FirstFreeTimeOnChangeBalans !=0).FirstOrDefault();
            if (vp != null)
            {
                vp.Status = "Not active";
                mp.Entry(vp).State = EntityState.Modified;
                mp.SaveChanges();
            }
            visitparameters nvis = mp.visitparameters.Where(x => x.FirstFreeTimeInMinutes==0 & x.FirstFreeTimeOnChangeBalans == 0).FirstOrDefault();
            if (nvis != null)
            {
                nvis.Status = "Active";
                mp.Entry(nvis).State = EntityState.Modified;
                mp.SaveChanges();
            }
            else
            {
                visitparameters vnew = new visitparameters();
                vnew.Status = "Active";
                vnew.FirstFreeTimeInMinutes = 0;
                vnew.FirstFreeTimeOnChangeBalans = 0;
                mp.visitparameters.Add(vnew);
                mp.SaveChanges();
            }

            return RedirectToAction("VisitParameters");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_visit_parameters", MatchFormValue = "Activate_parameters")]
        public ActionResult Activate_parameters(Int32 id_vis_param)
        {
            visitparameters vp = mp.visitparameters.Where(x => x.id_vis_param == id_vis_param).FirstOrDefault();
            if (vp != null)
            {
                visitparameters act = mp.visitparameters.Where(x => x.Status == "Active").FirstOrDefault();
                if (act != null)
                {
                    act.Status = "Not active";
                    mp.Entry(act).State = EntityState.Modified;
                    mp.SaveChanges();
                }
                vp.Status = "Active";
                mp.Entry(vp).State = EntityState.Modified;
                mp.SaveChanges();

            }
            return RedirectToAction("VisitParameters");
        }








        public ActionResult TariffsOnPlace()
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            ViewData["NotActiveTariffs"] = mp.tariffonplace.Where(x => x.Status != "Active");
            return View();
        }




        public ActionResult New_tariff_on_place()
        {
            //
            return RedirectToAction("TariffsOnPlace");
        }

        public ActionResult Edit_tariff_on_place()
        {
            //
            return RedirectToAction("TariffsOnPlace");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_tariff_on_place", MatchFormValue = "Edit_tariff_on_place")]
        public ActionResult Edit_tariff_on_place(Int32? id_tariff_on_place)
        {
            if (id_tariff_on_place != null)
            {
                tariffonplace tp = mp.tariffonplace.Where(x => x.id_tariff_on_place == id_tariff_on_place & x.Status == "Active").FirstOrDefault();
                if (tp != null)
                {
                    return View(tp);
                }
                else
                {
                    return RedirectToAction("TariffsOnPlace");
                }
            }
            else
            {
                return RedirectToAction("TariffsOnPlace");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "New_tariff_on_place", MatchFormValue = "New_tariff_on_place")]
        public ActionResult New_tariff_on_place2()
        {
            return View();
        }





        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "New_tariff_on_place", MatchFormValue = "Save_new_tariff_on_place")]
        public ActionResult Save_new_tariff_on_place(tariffonplace tp)
        {
            //ViewData["VisitParameters"] = "";
            //Наверное зедсь будет лучше если найдется запись с такими же параметрами в Not active, а создаваемые такие же будут Active,
            //Тогда перевести просто ту запись в Active.
            tariffonplace old = mp.tariffonplace.Where(x => x.SupportClimateControl == tp.SupportClimateControl & x.Type == tp.Type & x.PriceForHourWithAbonement == tp.PriceForHourWithAbonement & x.PriceForHourWithoutAbonement == tp.PriceForHourWithoutAbonement).FirstOrDefault();
            if (old != null)
            {
                if (old.Status == "Active")
                {
                    ViewData["VisitParameters"] = "Такой тариф уже существует и он активен";
                    return View(tp);
                }
                else if (old.Status != "Active")
                {

                    tariffonplace oldforchange = mp.tariffonplace.Where(x => x.SupportClimateControl== tp.SupportClimateControl & x.Type==x.Type & x.Status == "Active").FirstOrDefault();
                    if (tp.Status == "Active")
                    {
                        if (oldforchange != null)
                        {
                            oldforchange.Status = "Not active";
                            mp.Entry(oldforchange).State = EntityState.Modified;
                            mp.SaveChanges();
                        }
                    }

                    old.Status = tp.Status;
                    mp.Entry(old).State = EntityState.Modified;
                    mp.SaveChanges();
                }
            }
            else
            {
                tariffonplace oldforchange = mp.tariffonplace.Where(x => x.SupportClimateControl == tp.SupportClimateControl & x.Type == tp.Type & x.Status == "Active").FirstOrDefault();
                if (tp.Status == "Active")
                {
                    if (oldforchange != null)
                    {
                        oldforchange.Status = "Not active";
                        mp.Entry(oldforchange).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                }

                tariffonplace tpnew = new tariffonplace();
                tpnew.Status = tp.Status;
                tpnew.SupportClimateControl = oldforchange.SupportClimateControl;
                tpnew.Type = oldforchange.Type;
                tpnew.PriceForHourWithAbonement = tp.PriceForHourWithAbonement;
                tpnew.PriceForHourWithoutAbonement = tp.PriceForHourWithoutAbonement;
                mp.tariffonplace.Add(tpnew);
                mp.SaveChanges();
            }

            return RedirectToAction("TariffsOnPlace");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_tariff_on_place", MatchFormValue = "Change_tariff_on_place")]
        public ActionResult Change_visit_parameters(tariffonplace tp, Int32? id_tariff_on_place)
        {
            if (id_tariff_on_place != null)
            {
                tariffonplace oldforchange = mp.tariffonplace.Where(x => x.id_tariff_on_place == id_tariff_on_place).FirstOrDefault();
                if (oldforchange != null)
                {
                    tariffonplace old = mp.tariffonplace.Where(x => x.id_tariff_on_place == id_tariff_on_place & x.PriceForHourWithAbonement == tp.PriceForHourWithAbonement & x.PriceForHourWithoutAbonement == tp.PriceForHourWithoutAbonement).FirstOrDefault();
                    if (old != null)
                    {
                        ViewData["VisitParameters"] = "Изменений не произошло!";
                        return View(old);
                    }
                    else
                    {
                        if (oldforchange.Status != "Active")
                        {
                            ViewData["VisitParameters"] = "Этот тариф больше неактивен, применить к нему измененения невозможно!";
                            return View(old);
                        }
                        else
                        {
                            //произвожу изменение статуса тарифа на not active в oldforchange
                            tariffonplace old2 = mp.tariffonplace.Where(x =>x.SupportClimateControl == oldforchange.SupportClimateControl & x.Type==oldforchange.Type & x.Status!="Active" & x.PriceForHourWithAbonement == tp.PriceForHourWithAbonement & x.PriceForHourWithoutAbonement == tp.PriceForHourWithoutAbonement).FirstOrDefault();
             
                            oldforchange.Status = "Not active";
                            mp.Entry(oldforchange).State = EntityState.Modified;
                            mp.SaveChanges();
                            if (old2 != null)
                            {
                                    old2.Status = "Active";
                                    mp.Entry(old2).State = EntityState.Modified;
                                    mp.SaveChanges();
                                
                            }
                            else
                            {
                                tariffonplace tpnew = new tariffonplace();
                                tpnew.Status = "Active";
                                tpnew.SupportClimateControl = oldforchange.SupportClimateControl;
                                tpnew.Type = oldforchange.Type;
                                tpnew.PriceForHourWithAbonement = tp.PriceForHourWithAbonement;
                                tpnew.PriceForHourWithoutAbonement = tp.PriceForHourWithoutAbonement;
                                mp.tariffonplace.Add(tpnew);
                                mp.SaveChanges();
                            }
                        }
                    }
                }
            }
            return RedirectToAction("TariffsOnPlace");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_tariff_on_place", MatchFormValue = "Off_tariff")]
        public ActionResult Off_tariff(Int32 id_tariff_on_place)
        {
            tariffonplace tp = mp.tariffonplace.Where(x => x.id_tariff_on_place == id_tariff_on_place).FirstOrDefault();
            if (tp != null)
            {
                tp.Status = "Not active";
                mp.Entry(tp).State = EntityState.Modified;
                mp.SaveChanges();
            }
            return RedirectToAction("TariffsOnPlace");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Edit_tariff_on_place", MatchFormValue = "Activate_tariff")]
        public ActionResult Activate_tariff(Int32 id_tariff_on_place)
        {
            tariffonplace tp = mp.tariffonplace.Where(x => x.id_tariff_on_place == id_tariff_on_place).FirstOrDefault();
            if (tp != null)
            {
                tariffonplace act = mp.tariffonplace.Where(x => x.Status == "Active" & x.SupportClimateControl == tp.SupportClimateControl & x.Type == tp.Type).FirstOrDefault();
                if (act != null)
                {
                    act.Status = "Not active";
                    mp.Entry(act).State = EntityState.Modified;
                    mp.SaveChanges();
                }
                tp.Status = "Active";
                mp.Entry(tp).State = EntityState.Modified;
                mp.SaveChanges();

            }
            return RedirectToAction("TariffsOnPlace");
        }










//-------------------------------------------------ABONEMENTS: (without validation)

        public ActionResult TariffsOnAbonements()
        {
            ViewData["ActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status == "Available");
            ViewData["NotActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status != "Available");
            return View();
        }

        public ActionResult Create_abonement()
        {
            //
            return RedirectToAction("TariffsOnAbonements");
        }

        public ActionResult Change_abonement()
        {
            //
            return RedirectToAction("TariffsOnAbonements");
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Change_abonement", MatchFormValue = "Change_abonement")]
        public ActionResult Change_abonement(string Name_tariff)
        {
            tariffonabonementforvisit ab = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == Name_tariff).FirstOrDefault();
            if (ab != null)
            {
                ViewData["Name"] = Name_tariff;
                return View(ab);
            }
            else
            {
                return RedirectToAction("TariffsOnAbonements");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_abonement", MatchFormValue = "Create_abonement")]
        public ActionResult Create_abonement2()
        {
            return View();
        }






        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Create_abonement", MatchFormValue = "Save_abonement")]
        public ActionResult Save_abonement(tariffonabonementforvisit ab)
        {

            tariffonabonementforvisit oldforchange = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == ab.Name_tariff_on_abonement).FirstOrDefault();


            if (oldforchange == null)
            {

                tariffonabonementforvisit old2 = mp.tariffonabonementforvisit.Where(x => (x.Max_Num_visits_in_this_tariff == ab.Max_Num_visits_in_this_tariff || x.Num_days == ab.Num_days) & x.Price == ab.Price).FirstOrDefault();
                if (old2 == null)
                {
                    tariffonabonementforvisit abnew = new tariffonabonementforvisit();
                    abnew.Status = ab.Status;
                    abnew.Max_Num_visits_in_this_tariff = ab.Max_Num_visits_in_this_tariff;
                    abnew.Name_tariff_on_abonement = ab.Name_tariff_on_abonement;
                    abnew.Num_days = ab.Num_days;
                    abnew.Price = ab.Price;

                    mp.tariffonabonementforvisit.Add(abnew);
                    mp.SaveChanges();
                }
                else if (old2 != null)
                {
                    if (old2.Status != "Available" & ab.Status == "Available")
                    {
                        old2.Status = "Available";
                        mp.Entry(old2).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                }

            }





            return RedirectToAction("TariffsOnAbonements");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Change_abonement", MatchFormValue = "Save_change_abonement")]
        public ActionResult Change_visit_parameters(tariffonabonementforvisit ab, string Name)
        {//string name
            tariffonabonementforvisit oldforchang = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == Name).FirstOrDefault();
            if (oldforchang != null)
            {
                oldforchang.Status = "Not available";
                mp.Entry(oldforchang).State = EntityState.Modified;
                mp.SaveChanges();
                tariffonabonementforvisit old2 = mp.tariffonabonementforvisit.Where(x => (x.Max_Num_visits_in_this_tariff == ab.Max_Num_visits_in_this_tariff || x.Num_days == ab.Num_days) & x.Price == ab.Price).FirstOrDefault();
                if (old2 != null)
                {
                    if (old2.Status != "Available")
                    {
                        old2.Status = "Available";
                        mp.Entry(old2).State = EntityState.Modified;
                        mp.SaveChanges();
                    }
                }
                else
                {
                    tariffonabonementforvisit oldforchange = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == ab.Name_tariff_on_abonement).FirstOrDefault();



                    if (oldforchange == null)
                    {
                        tariffonabonementforvisit abnew = new tariffonabonementforvisit();
                        abnew.Status = "Available";
                        abnew.Max_Num_visits_in_this_tariff = ab.Max_Num_visits_in_this_tariff;
                        abnew.Name_tariff_on_abonement = ab.Name_tariff_on_abonement;
                        abnew.Num_days = ab.Num_days;
                        abnew.Price = ab.Price;

                        mp.tariffonabonementforvisit.Add(abnew);
                        mp.SaveChanges();
                    }
                }


            }



            return RedirectToAction("TariffsOnAbonements");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Change_abonement", MatchFormValue = "Off_abonement")]
        public ActionResult Off_abonement(string Name_tariff)
        {
            tariffonabonementforvisit ab = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == Name_tariff & x.Status == "Available").FirstOrDefault();
            if (ab != null)
            {
               ab.Status = "Not available";
                mp.Entry(ab).State = EntityState.Modified;
                mp.SaveChanges();
            }
            return RedirectToAction("TariffsOnAbonements");
        }



        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "Change_abonement", MatchFormValue = "Activate_abonement")]
        public ActionResult Activate_abonement(string Name_tariff)
        {
            tariffonabonementforvisit ab = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == Name_tariff).FirstOrDefault();
            if (ab != null)
            {

                ab.Status = "Available";
                mp.Entry(ab).State = EntityState.Modified;
                mp.SaveChanges();

            }
            return RedirectToAction("TariffsOnAbonements");
        }


        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "TariffsOnAbonements", MatchFormValue = "Choose_abonement")]
        public ActionResult ChooseTariffOnAbonements()
        {
            //Авторизован и роль Driver
            usingtariffonabonementforvisit expiredabonement = new usingtariffonabonementforvisit();
            expiredabonement.ExpiredAbonement(User.Identity.Name);
            usingtariffonabonementforvisit usingabtar = mp.usingtariffonabonementforvisit.Where(x => x.Login == User.Identity.Name & x.Status == "Active").FirstOrDefault();
            if (usingabtar == null)
            {
                ViewData["Choose"] = "ВЫБОР АБОНЕМЕНТА";
            }
            else
            {
                ViewData["Choose"] = "";
                ViewData["AbonementError"] = "Поскольку Вы уже имеете активный подключенный абонемент - Вы были перенаправлены на обычный просмотр абонементов.";
            }
            ViewData["ActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status == "Available");
            ViewData["NotActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status != "Available");
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "TariffsOnAbonements", MatchFormValue = "Connect_abonement")]
        public ActionResult ConnectTariffOnAbonements(string Name_tariff)
        {
            //Есл авторизован и является ролью Driver
            tariffonabonementforvisit abtar = mp.tariffonabonementforvisit.Where(x => x.Name_tariff_on_abonement == Name_tariff & x.Status == "Available").FirstOrDefault();
            if (abtar != null)
            {
                string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                string Login = User.Identity.Name;
                //Продолжаю
                //Подключить(создать) запись
                //СПисать с юзера баланс
                //Добавить запись в операции над балансом
                usingtariffonabonementforvisit usingabtar = new usingtariffonabonementforvisit();
                if (usingabtar.Connection(abtar.Name_tariff_on_abonement, Date, Login))
                {
                    usr searchusr = mp.usr.Where(x => x.Login == Login).FirstOrDefault();
                    decimal Now_balance = (decimal)searchusr.Now_Balance - abtar.Price;
                    searchusr.Now_Balance = Now_balance;
                    mp.Entry(searchusr).State = EntityState.Modified;
                    mp.SaveChanges();

                    balance connectabonement = new balance();
                    connectabonement.Operation("Debit", abtar.Price, Now_balance, Login, "Connect abonemement", Date);
                    

                        return RedirectToAction("AbonementList", new { Controller = "Abonement" });
                    
                }
            }
            else
            {
                ViewData["ActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status == "Available");
                ViewData["NotActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status != "Available");
                ViewData["AbonementError"] = "Выбранный вами абонемент на данный момент стал неактивным, но Вы можете выбрать другой абонемент.";
                ViewData["Choose"] = "ВЫБОР АБОНЕМЕНТА";
                return View();
            }
            ViewData["AbonementError"] = "Внимание, возникла непредвиденная ошибка, пожалуйста, попробуйте повторить ваше действие и в случае повторной неудачи обратитесь к администратору с описанием проблемы!";
            ViewData["Choose"] = "ВЫБОР АБОНЕМЕНТА";
            return View();
        }


    }
}
