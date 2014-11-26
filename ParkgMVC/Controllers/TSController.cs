using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkgMVC.Models;
namespace ParkgMVC.Controllers
{
    public class TSController : Controller
    {
        //
        // GET: /TS/

        MyParkingEntities mp = new MyParkingEntities();
        public ActionResult TS()
        {
            if (User.Identity.IsAuthenticated)
            {
                string Log = User.Identity.Name.ToString();
                return View(mp.ts.Where(x => x.Login == Log & x.Status == "True").ToList());
            }
            else
            {
                return RedirectToAction("LogOn", new { Controller = "Account" });
            }
        }
        
        public ActionResult CreateTS()
        {
                        if (User.Identity.IsAuthenticated)
            {
            return View();
            }
            else
            {
                            return RedirectToAction("LogOn", new { Controller = "Account" });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTS(ts newts)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ts ts = new ts();
                    string Log = User.Identity.Name.ToString();
                    if (ts.CreateTS(newts, Log))
                        return RedirectToAction("TS");
                    else
                        return View("CreateTS");
                }
                else
                    return View("CreateTS");
            }
            catch
            {
                return View("CreateTS");
            }
        }
        
        public ActionResult DeleteTS(Int32 Id_ts, string Company)
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DeleteTS(Int32 Id_ts)
        {
            try
            {
                ts ts = new ts();
                string Log = User.Identity.Name.ToString();
                if (ts.ChangeStatus(Id_ts, Log))
                    return RedirectToAction("TS");
                else
                {
                    ViewData["Error"] = "Вы не можете удалить это ТС, поскольку у Вас подключена заявка на бронирование, которая требует наличие минимум одного ТС!";
                    return View("DeleteTS");
                }
            }
            catch
            {
                return View("DeleteTS");
            }
        }
    }
}
