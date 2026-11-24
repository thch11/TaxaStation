using System;
using System.Collections.Generic;
using System.Text;

namespace TaxaStation
{
    public class CarShiftOption
    {
        public Car Car { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }

        public override string ToString()
        {
            return Car.CarBrand + " " + Car.LicensePlate + " " + StartHour + ":00 - " + EndHour + ":00";
        }
    }
}
