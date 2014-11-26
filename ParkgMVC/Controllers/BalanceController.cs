using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkgMVC.Models;
namespace ParkgMVC.Controllers
{
    public class BalanceController : Controller
    {
        //
        // GET: /Balance/
        MyParkingEntities mp = new MyParkingEntities();
        public ActionResult Balance()
        {
            string Log = User.Identity.Name.ToString();
            return View(mp.balance.Where(x=>x.Login == Log).ToList());
        }

    }
}
