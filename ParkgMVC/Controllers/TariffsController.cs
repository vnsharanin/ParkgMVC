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
        public ActionResult TariffsOnPlace()
        {
            ViewData["ActiveTariffs"] = mp.tariffonplace.Where(x => x.Status == "Active");
            ViewData["NotActiveTariffs"] = mp.tariffonplace.Where(x => x.Status != "Active");
            return View();
        }

        public ActionResult VisitParameters()
        {
            ViewData["ActiveParameters"] = mp.visitparameters.Where(x => x.Status == "Active");
            ViewData["NotActiveParameters"] = mp.visitparameters.Where(x => x.Status != "Active");
            return View();
        }


        public ActionResult TariffsOnAbonements()
        {
            ViewData["ActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status == "Available");
            ViewData["NotActiveTariffs"] = mp.tariffonabonementforvisit.Where(x => x.Status != "Available");
            return View();
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
