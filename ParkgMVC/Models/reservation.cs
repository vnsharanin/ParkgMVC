﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkgMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data;
    using System.Data.Entity;
    using ParkgMVC.Models;
    using System.ComponentModel.DataAnnotations;
    public partial class reservation : ConnectedTariffPlan
    {
        public long id_reservation_user { get; set; }
        public long id_Reservation_Tariff { get; set; }
        public Nullable<long> id_location_place { get; set; }
        public Nullable<int> id_alternative_location_place { get; set; }
        public string ApproximatelyDateOutFromActivity { get; set; }
        public string Description { get; set; }

        public virtual place place { get; set; }
        public virtual reservation_tariff reservation_tariff { get; set; }
        public virtual usr usr { get; set; }

        MyParkingEntities mp = new MyParkingEntities();

        public bool CreateReservation(string Describe, string Log, reservation_tariff tar)
        {
            bool Result = false;
            try
            {
                reservation r = new reservation();
                r.id_Reservation_Tariff = tar.id_Reservation_Tariff;
                r.Login = Log;
                r.Status = "Formed";
                r.Description = Describe;
                mp.reservation.Add(r);
                mp.SaveChanges();
                Result = true;
            }
            catch
            {
                Result = false;
            }
            return Result;
        }

        //не заработал. оставлю для алгоритма экстренной смены статуса места при забронированном состоянии
        public void Edit(reservation formedres, Int32 id_loc_place)
        {
            formedres.id_location_place = id_loc_place;
            formedres.id_alternative_location_place = id_loc_place;
            mp.Entry(formedres).State = EntityState.Modified;
            mp.SaveChanges();
        }

        public bool Revoke(string Describe, reservation obj, string Date)
        {
            bool result = false;
            try
            {
                //format my date have view:  string d = "21.11.14 20:00";

                decimal price = 0;
                usr ur = mp.usr.Where(x => x.Login == obj.Login).FirstOrDefault();
                balance bl = new balance();
                place p = new place();

                //Можно упростить задачу. Следующим коментируемым кодом
                /*  obj.id_alternative_location_place == obj.id_location_place
                 * то есть если место уже изменено на другое, то сделать автомобилисту бесплатную бронь(т.к. нарушена его потребность)
                 * соответственно проверка на то что лучше это место или хуже не понадобится вообще.
                 * 
                 * сейчас действует схема, что бронь бесплатна если новое выданное место хуже или его нет вообще. (риск он принимал в
                 * соглашении)
                */
                if (obj.id_alternative_location_place != null) {


                    if (Describe != "Reservation was used")
                    {
                        //освобождающее место либо достанется кому-то, либо переведтся в свободное состояние
                        p.FreePlace((long)obj.id_alternative_location_place);
                    }

                    //Эта проверка может непонадобится, если обдумать описанный комментарий выше.
                    if (p.bestornot((long)obj.id_location_place, (long)obj.id_alternative_location_place) == true ||
                        obj.id_alternative_location_place == obj.id_location_place)
                        {


                long span = 0;

                if (Describe == "Reservation was expired" | Describe == "Reservation was revoke")
                {
                    span = Convert.ToDateTime(Date).Ticks - Convert.ToDateTime(obj.DateConnection).Ticks;
                }
                else if (Describe == "Reservation was used")
                {



                    DateTime mydate = Convert.ToDateTime(obj.DateConnection).AddMinutes(obj.reservation_tariff.FirstFreeTimeInMinutes);
                    if (mydate <= Convert.ToDateTime(DateTime.Now.ToString("dd.MM.yy HH:mm")))
                    {
                        span = Convert.ToDateTime(Date).Ticks - mydate.Ticks;
                    }


                }





                    decimal priceinmin = (decimal)(obj.reservation_tariff.PriceInRubForHourHightFreeTime) / 60;

                            price = (decimal)TimeSpan.FromTicks(span).TotalMinutes * priceinmin;
                            ur.Now_Balance = ur.Now_Balance - price;
                            mp.Entry(ur).State = EntityState.Modified;
                            mp.SaveChanges();
                        }

                    
                    //Да и еще, расчеты делаются точно. Но не округлить ли баланс на выходе в лучшую сторону для автомобилиста, до копейки(сотых) хотя бы.
                }

                
                bl.Operation("Debit", price, (decimal)ur.Now_Balance, ur.Login, Describe, Date);
                
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool FindOnExpired(string Log)
        {
            if (Log != "")
            {
                bool Result = false;
                foreach (reservation n in mp.reservation.Where(x => x.Login == Log & x.Status == "Active").ToList())
                {
                string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                    //Проверяю истекла ли дата брони
                    if (Convert.ToDateTime(n.ApproximatelyDateOutFromActivity) < Convert.ToDateTime(Date))
                    {
                        n.DateOutFromActivity = n.ApproximatelyDateOutFromActivity;
                        n.Status = "Closed";
                        n.Description = "Reservation was expired";
                        mp.Entry(n).State = EntityState.Modified;
                        mp.SaveChanges();
                        //При посещении или отказе (и если бронь не истекла) в кач-ве третьего параметра отправить текущее время,
                        //Здесь оа истекла и я отправляю предположительное, уже ранее рассчитанное при создании заявки брони.
                        reservation r = new reservation();
                        r.Revoke("Reservation was expired", n, n.ApproximatelyDateOutFromActivity);
                        //рассчитать средства и списать их со счета.
                        Result = true;
                        break;
                    }
                }
                return Result;
            }
            else
            {

                foreach (reservation n in mp.reservation.Where(x => x.Status == "Active").ToList())
                {
                    string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
                    //Проверяю истекла ли дата брони
                    if (Convert.ToDateTime(n.ApproximatelyDateOutFromActivity) < Convert.ToDateTime(Date))
                    {
                        n.DateOutFromActivity = n.ApproximatelyDateOutFromActivity;
                        n.Status = "Closed";
                        n.Description = "Reservation was expired";
                        mp.Entry(n).State = EntityState.Modified;
                        mp.SaveChanges();
                        //При посещении или отказе (и если бронь не истекла) в кач-ве третьего параметра отправить текущее время,
                        //Здесь оа истекла и я отправляю предположительное, уже ранее рассчитанное при создании заявки брони.
                        reservation r = new reservation();
                        r.Revoke("Reservation was expired", n, n.ApproximatelyDateOutFromActivity);
                        //рассчитать средства и списать их со счета.
                        break;
                    }
                }
                return true;
            }


        }

    }
}
