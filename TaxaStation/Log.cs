using System;
using System.Collections.Generic;
using System.Text;

namespace TaxaStation
{
    public class Log
    {
        public Driver Driver { get; private set; }
        public string Time { get; private set; }
        public string Action { get; private set; }     
        
        public Log(Driver driver, string time, string action)
        {
            Driver = driver;
            Time = time;
            Action = action;
        }
    }
}
