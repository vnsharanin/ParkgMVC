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
    using ParkgMVC.Models;
    using System.Data;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations;

    public partial class place : Statechart
    {
        public place()
        {
            this.reservation = new HashSet<reservation>();
            this.visit = new HashSet<visit>();
        }
        public long id_location_place { get; set; }
        public long id_location_level { get; set; }
        public int NumberPlace { get; set; }
        public long id_tariff_on_place { get; set; }
        public Nullable<int> id_alternative_tariff_on_place { get; set; }

        public virtual levelzone levelzone { get; set; }
        public virtual tariffonplace tariffonplace { get; set; }
        public virtual ICollection<reservation> reservation { get; set; }
        public virtual ICollection<visit> visit { get; set; }

        MyParkingEntities mp = new MyParkingEntities();

        public bool ChangeStatus(string newstatus, long id_location_place, Int32 alt_tariff)
        {
            place ForChangeStatus = mp.place.Where(x => x.id_location_place == id_location_place).FirstOrDefault();

            ForChangeStatus.Status = newstatus;
            if (alt_tariff != 0)
            {
                ForChangeStatus.id_alternative_tariff_on_place = alt_tariff;
            }
            else
            {
                ForChangeStatus.id_alternative_tariff_on_place = (int)ForChangeStatus.id_tariff_on_place;
            }
            mp.Entry(ForChangeStatus).State = EntityState.Modified;
            mp.SaveChanges();
            return true;
        }

        //Нужен для регистрации выезда, так же сойдет для отката(и истечения) брони:   
         //Можно попробовать задействовать при включении места. Мое место  включить и сразу все обновить, а альтернативное
        //уже не нужное отправить сюда
        public bool FreePlace(long free_id_loc_place)
        {
            /* Может сделать следующую штуку?
             Если все брони покрыты местами, null нет ни у кого. То заняться поиском тех броней у которых
             альтернативное и первоначальное место отличаются и если альтернативное хуже изначального, тогда
             проверить может быть это будет лучше или такое(по тарифу) и если это освобождающееся окажется лучше, то
             ту альтернативу затереть (месту присвоить free, альт-й тариф сделать равным первоначальному) в рекурсию FreePlace
             и теперь в аллтернативе расп-я в броне указываю это расположение, а в месте указ-ю соотв-й тариф и меняю статус
             
             */
            bool res = true;
            place fp = mp.place.Where(x => x.id_location_place == free_id_loc_place).FirstOrDefault();
            var obj = mp.reservation.Where(x => x.Status == "Active" & x.id_alternative_location_place == null).ToList();
            int count = obj.Count();
            place pl = new place();
            if (count > 0)
            {
                long id_user_reserv = 0;
                int i = 0;
                foreach (var search in obj)
                {
                    DateTime min = Convert.ToDateTime(search.DateConnection);
                    foreach (var mindate in obj)
                    {
                        if (i < count)
                        {
                            if (min <= Convert.ToDateTime(mindate.DateConnection))
                            {
                                min = Convert.ToDateTime(mindate.DateConnection);
                                id_user_reserv = mindate.id_reservation_user;
                            }
                            i++;
                        }
                        else { break; }
                    }
                    break;
                }
                reservation myfind = mp.reservation.Where(x => x.id_reservation_user == id_user_reserv).FirstOrDefault();
                myfind.id_alternative_location_place = (int)free_id_loc_place;
                mp.Entry(myfind).State = EntityState.Modified;
                mp.SaveChanges();
                //Если освобождающееся место лучше или такое же
                if (pl.bestornot((long)myfind.id_location_place, free_id_loc_place) == true)
                {
                    pl.ChangeStatus("In waiting visit", free_id_loc_place, (int)myfind.place.id_tariff_on_place);
                }
                //если нет
                else
                {
                    pl.ChangeStatus("In waiting visit", free_id_loc_place, 0);
                }

               res = true;
            }
            else
            {
                int newfr = 0;
                var eq = mp.reservation.Where(x =>  x.Status == "Active" & x.id_alternative_location_place != x.id_location_place & x.id_alternative_location_place != free_id_loc_place).ToList();
               int e = eq.Count();
                //long id_u_r = 0;
               // int coun = 0;

                if (e > 0)
                {
                    foreach (var s1 in eq)
                    {
                        place fineq = mp.place.Where(x => x.id_location_place == s1.id_alternative_location_place).FirstOrDefault();

                            if ((fp.tariffonplace.PriceForHourWithoutAbonement > fineq.tariffonplace.PriceForHourWithoutAbonement) &
                             (fp.tariffonplace.PriceForHourWithoutAbonement <= s1.place.tariffonplace.PriceForHourWithoutAbonement))
                                //alt<new<=first 
                            {
                                //Можно модернизировать путем определения ранней даты. Кто раньше забронировался, тому и получше достанется
                                //присвоить и получить
                                newfr = (int)s1.id_alternative_location_place;
                                s1.id_alternative_location_place = (int)free_id_loc_place;
                                mp.Entry(s1).State = EntityState.Modified;
                                mp.SaveChanges();
                               pl.ChangeStatus("In waiting visit", free_id_loc_place, (int)fp.id_tariff_on_place);          
                               break;
                            }

                            
                        
                    }//Разъединил. Сначала те, кто могут подняться к лучшему, потом те, кто могут отпустить более лучшее место, потом
                    //те, кто могут из худшего перейти в лучшее сразу. Может быть удалить первую часть, оставив только 3-ю вместо нее?
                    if (newfr == 0)//последнее добавленное
                    {
                        foreach (var s1 in eq)
                        {
                            place fineq = mp.place.Where(x => x.id_location_place == s1.id_alternative_location_place).FirstOrDefault();
                            if ((fp.tariffonplace.PriceForHourWithoutAbonement >= s1.place.tariffonplace.PriceForHourWithoutAbonement) &
                            (fp.tariffonplace.PriceForHourWithoutAbonement < fineq.tariffonplace.PriceForHourWithoutAbonement))
                            {//alt>new>=first
                                newfr = (int)s1.id_alternative_location_place;
                                s1.id_alternative_location_place = (int)free_id_loc_place;
                                mp.Entry(s1).State = EntityState.Modified;
                                mp.SaveChanges();
                                pl.ChangeStatus("In waiting visit", free_id_loc_place, (int)s1.place.id_tariff_on_place);
                            break;
                            }
                            
                        }
                    }


                    if (newfr == 0)//последнее добавленное
                    {
                        foreach (var s1 in eq)
                        {// alt<first<=new
                            place fineq = mp.place.Where(x => x.id_location_place == s1.id_alternative_location_place).FirstOrDefault();
                            if (((s1.place.tariffonplace.PriceForHourWithoutAbonement > fineq.tariffonplace.PriceForHourWithoutAbonement) &
                                 (fp.tariffonplace.PriceForHourWithoutAbonement >= s1.place.tariffonplace.PriceForHourWithoutAbonement)))
                            {
                                newfr = (int)s1.id_alternative_location_place;
                                s1.id_alternative_location_place = (int)free_id_loc_place;
                                mp.Entry(s1).State = EntityState.Modified;
                                mp.SaveChanges();
                                pl.ChangeStatus("In waiting visit", free_id_loc_place, (int)fp.id_tariff_on_place);
                                break;
                            }


                        }
                    }
                }
                    if (newfr != 0)
                    {
                        pl.FreePlace((long)newfr);
                    }
                    else
                    {
                        pl.ChangeStatus("Free", free_id_loc_place, 0);
                        res = false ;
                    }
                
                
            }
            return res;
        }

        public bool bestornot(long first_place, long new_place)
        {
            place fp = mp.place.Where(x => x.id_location_place == first_place).FirstOrDefault();
            place np = mp.place.Where(x => x.id_location_place == new_place).FirstOrDefault();
            if (fp.tariffonplace.PriceForHourWithoutAbonement <= np.tariffonplace.PriceForHourWithoutAbonement)
            {
                return true;
            }
            else return false;
        }





    }
}
