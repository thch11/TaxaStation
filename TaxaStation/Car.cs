using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxaStation
{
    public class Car
    {
        [JsonInclude]
        public string CarBrand { get; private set; }
        [JsonInclude]
        public string LicensePlate { get; private set; }
        [JsonInclude]
        public int KMdriven { get; private set; }

        public Car(string carBrand, string licenseplate, int kmDriven)
        {
            CarBrand = carBrand;
            LicensePlate = licenseplate;
            KMdriven = kmDriven;
        }

        public override string ToString()
        {
            return CarBrand + " " + LicensePlate;
        }
       
    }
}
