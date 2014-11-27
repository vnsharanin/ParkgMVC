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
                ViewData["Error"] = "";
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
                    if (ts.CreateTS(newts, Log) == "1")
                        return RedirectToAction("TS");
                    else
                    {
                        ViewData["Code"] = ts.CreateTS(newts, Log);
                        return View("CreateTS");
                    }
                }
                else
                    return View("CreateTS");
            }
            catch
            {
                return View("CreateTS");
            }
        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        [MultiButton(MatchFormKey = "TS", MatchFormValue = "Delete_TS")]
        public ActionResult Delete_TS(Int32 id_ts)
        {
            string Log = User.Identity.Name.ToString();
            ViewData["Error"] = "";
            try
            {
                ts ts = new ts();
                
                if (ts.ChangeStatus(id_ts, Log)=="1")
                    return View(mp.ts.Where(x => x.Login == Log & x.Status == "True").ToList());
                else
                {
                    ViewData["Error"] = ts.ChangeStatus(id_ts, Log);
                    return View(mp.ts.Where(x => x.Login == Log & x.Status == "True").ToList());
                }
            }
            catch
            {
                return View(mp.ts.Where(x => x.Login == Log & x.Status == "True").ToList());
            }
        }
    }
}
