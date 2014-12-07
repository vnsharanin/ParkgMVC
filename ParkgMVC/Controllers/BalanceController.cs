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


        public List<string> Operation()
        {
            List<string> Lis = new List<string>();

            Lis.Add("Replenishment");
            Lis.Add("Debit");
            return Lis;
        }


        public ActionResult cashFlow()
        {
            //ViewData["OpList"] = new SelectList(Operation(), "");
            balance op = new balance();
            return View(op);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult cashFlow(balance op, string Number)
        {
            string Date = DateTime.Now.ToString("dd.MM.yy HH:mm");
            string Description = "";
            decimal nowbalance = 0;
            if (op.Login == "" || op.Login == null || op.Login == " ")
            {
                NumberAttribute na = new NumberAttribute();
                try
                {
                    na.Validate(Number,"Error");
                    if (string.IsNullOrEmpty(Number))
                    {
                       ViewData["Message"] = "Не заполнен получатель (введите номер его ТС или логин)!";
                       return View("cashFlow");
                    }
                    else
                    {
                        if (ModelState.IsValidField("Sum"))
                        {
                            if (op.Sum != 0)
                            {
                                ts searchts = mp.ts.Where(x => x.Number == Number & x.Status == "True").FirstOrDefault();
                                if (searchts != null)
                                {
                                    usr searchusr = mp.usr.Where(x => x.Login == searchts.Login).FirstOrDefault();


                                    if (op.Type_Operation == "Debit" & searchusr.Now_Balance > 0)
                                    {
                                        nowbalance = (decimal)searchusr.Now_Balance - op.Sum;
                                        if (nowbalance < 0)
                                        {
                                            ViewData["Message"] = "Невозможно вывести запрашиваемую сумму, т.к. она превышает допустимый счет! Максимальная целая сумма доступная для вывода: " + Math.Floor((decimal)searchusr.Now_Balance) + " rub";
                                            return View("cashFlow");
                                        }
                                        Description = "Return money from account";
                                    }
                                    else if (op.Type_Operation == "Debit" & searchusr.Now_Balance <= 0)
                                    {
                                        ViewData["Message"] = "Для того, что бы вывести денежные средства, необходимо иметь на счете больше, чем 0 руб. Сейчас на счете: " + searchusr.Now_Balance;
                                        return View("cashFlow");
                                    }
                                    if (op.Type_Operation != "Debit")
                                    {
                                        nowbalance = (decimal)searchusr.Now_Balance + op.Sum;
                                        Description = "The receipt of money";
                                    }
                                    searchusr.Now_Balance = nowbalance;
                                    mp.Entry(searchusr).State = EntityState.Modified;
                                    mp.SaveChanges();
                                    balance newop = new balance();
                                    newop.Operation(op.Type_Operation, op.Sum, nowbalance, searchusr.Login, Description, Date);
                                    return RedirectToAction("Index", new { Controller = "Home" });
                                }
                                else
                                {
                                    ViewData["Message"] = "Введенный вами номер ТС не найден в базе!";
                                    return View("cashFlow");
                                }
                            }
                            else
                            {
                                ViewData["Message"] = "Сумма должна быть больше, чем 0!";
                                return View("cashFlow");
                            }
                        }
                        else
                        {
                            return View("cashFlow");
                        }
                    }
                }
                catch
                {
                    ViewData["Message"] = "Введенный номер '" + Number + "' несоответсвует стандарту.";
                    return View("cashFlow");

                }
            }
            else if (op.Login != "" & op.Login != " " & (Number == "" || Number == null))
            {
                if (op.Type_Operation != "Debit")
                {

                    if (ModelState.IsValidField("Sum"))
                    {
                        if (op.Sum != 0)
                        {
                            usr searchusr = mp.usr.Where(x => x.Login == op.Login).FirstOrDefault();
                            if (searchusr != null)
                            {
                                nowbalance = (decimal)searchusr.Now_Balance + op.Sum;
                                Description = "The receipt of money";
                                searchusr.Now_Balance = nowbalance;
                                mp.Entry(searchusr).State = EntityState.Modified;
                                mp.SaveChanges();
                                balance newop = new balance();
                                newop.Operation(op.Type_Operation, op.Sum, nowbalance, searchusr.Login, Description, Date);
                            }
                            else
                            {
                                ViewData["Message"] = "Логин '" + op.Login + "' не найден в базе!";
                                return View("cashFlow");
                            }



                        return RedirectToAction("Index", new { Controller = "Home" });
                        }
                        else
                        {
                            ViewData["Message"] = "Сумма должна быть больше, чем 0!";
                            return View("cashFlow");
                        }

                    }
                    else
                    {
                        return View("cashFlow");
                    }
                }
                else
                {
                    ViewData["Message"] = "Вывод средств по логину запрещен!Напоминание: при выводе средств по номеру необходимы документы, которые смогут удостоверить, что данное ТС действительно принадлежит лицу, пытающемуся совершить вывод средств. Т.е. лицо, пытающееся совершить вывод средств должно являться собственником указанного ТС. Так же необходимо проверить соответсвие между этими данными с паспортными.";
                    return View("cashFlow");
                }

            }
            return View("cashFlow");
        }
    }
}
