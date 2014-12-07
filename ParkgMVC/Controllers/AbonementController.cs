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
    public class AbonementController : Controller
    {
        //
        // GET: /Abonent/
        MyParkingEntities mp = new MyParkingEntities();

        public ActionResult AbonementList()
        {
            //Авторизован и роль Driver
            //Проверка на истечение абонемента
            usingtariffonabonementforvisit expiredabonement = new usingtariffonabonementforvisit();
            expiredabonement.ExpiredAbonement(User.Identity.Name);
            ViewData["ActiveAbonement"] = mp.usingtariffonabonementforvisit.Where(x => x.Status == "Active" & x.Login == User.Identity.Name);
            ViewData["NotActiveAbonements"] = mp.usingtariffonabonementforvisit.Where(x => x.Status != "Active" & x.Login == User.Identity.Name);
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "AbonementList", MatchFormValue = "Revoke_abonement")]
        public ActionResult RevokeAbonement(Int64 id_abonement)
        {
            //Авторизован и роль Driver
            string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
            usingtariffonabonementforvisit expiredabonement = new usingtariffonabonementforvisit();
            usingtariffonabonementforvisit activeab = mp.usingtariffonabonementforvisit.Where(x => x.id_abonement == id_abonement).FirstOrDefault();
            if (expiredabonement.ExpiredAbonement(User.Identity.Name) == true)
            {
                ViewData["MyAbonementMessage"] = "Срок действия вашего абонемента истек до его собственнорчного закрытия.";
            }
            else
            {
                activeab.DateOutFromActivity = Date;
                activeab.Status = "Revoke";
                mp.Entry(activeab).State = EntityState.Modified;
                mp.SaveChanges();
                ViewData["MyAbonementMessage"] = "Абонемент был успешно закрыт";
            }
            ViewData["ActiveAbonement"] = mp.usingtariffonabonementforvisit.Where(x => x.Status == "Active" & x.Login == User.Identity.Name);
            ViewData["NotActiveAbonements"] = mp.usingtariffonabonementforvisit.Where(x => x.Status != "Active" & x.Login == User.Identity.Name);
            return View();
        }
    }
}
