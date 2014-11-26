using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkgMVC.Models
{
    public class Statechart
    {
        public string Status { get; set; }

        public bool ChangeStatus()
        {
            return true;
        }
    }
}