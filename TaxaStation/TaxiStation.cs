using System;
using System.Collections.Generic;
using System.Text;

namespace TaxaStation
{
    public class TaxiStation
    {
        public List<Driver> DriverList { get; set; } = new List<Driver>();

        public Driver AddDriver(string name, int workstart, int workend)
        {
           Driver driver = AddDriverToList(CreateDriver(name, workstart, workend));
           return driver;
        }
        private Driver CreateDriver(string name, int workstart, int workend)
        {
            Driver driver = new Driver(name, workstart, workend);
            return driver;
        }
        private Driver AddDriverToList(Driver driver)
        {
            DriverList.Add(driver);
            return driver;

        }

    }
}
