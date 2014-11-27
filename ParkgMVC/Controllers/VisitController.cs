using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkgMVC.Controllers
{
    public class VisitController : Controller
    {
        //
        // GET: /Visit/

        public ActionResult ListVisit()
        {
            return View();
        }
        //Учесть броню проверить на свободность места, которое занято по первому id, если занято, то пустить по альтернативному
        public ActionResult BalanceOperation()
        {
            return View();
        }

    }
}
