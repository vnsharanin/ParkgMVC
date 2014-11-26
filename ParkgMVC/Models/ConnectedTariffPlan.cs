using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkgMVC.Models
{
    public class ConnectedTariffPlan : Statechart
    {
        public string Login { get; set; }
        public string DateConnection { get; set; }
        public string DateOutFromActivity { get; set; }

        public bool Connection()
        {
            return true;
        }

    }
}