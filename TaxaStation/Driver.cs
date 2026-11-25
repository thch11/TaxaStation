using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxaStation
{
    public class Driver
    {
        [JsonInclude]
        public string Name { get; private set; }
        [JsonInclude]
        public int WorkStart { get; private set; }
        [JsonInclude]
        public int WorkEnd { get; private set; }
        [JsonInclude]
        private string Availability { get; set; } = "Available";
        [JsonInclude]
        public Car driversCar { get; private set; }
        [JsonInclude]
        public int WorkTime { get; private set; }

        public Driver(string name, int workstart, int workend, Car DriversCar)
        {
            Name = name;
            WorkStart = workstart;
            WorkEnd = workend;
            driversCar = DriversCar;
        }

        public void RemoveCar()
        {
            this.driversCar = null;
        }
        public void AddWorkTime(int minutes)
        {
            this.WorkTime = this.WorkTime + minutes;
        }
        public override string ToString()
        {
            if(driversCar != null)
            {
                return Name + " " + " | " + Availability + " | " + WorkStart + " - " + WorkEnd + " | " + driversCar.CarBrand + " - " + driversCar.LicensePlate;
            }
            return Name + " " + " | " + Availability + " | " + WorkStart + " - " + WorkEnd + " | " + "No car assigned";

        }

        public void AssignCarAndShift(Car car, int startHour, int endHour)
        {
            this.driversCar = car;
            this.WorkStart = startHour;
            this.WorkEnd = endHour;
        }

        public void ChangeAvailability(string a)
        {
            this.Availability = a;
        }

        public string GetAvailability()
        {
            return this.Availability;
        }

        public string GetName()
        {
            return this.Name;
        }

        public void AddMinutes(int minutes)
        {
            WorkTime = WorkTime + minutes;
        }

    }
}
