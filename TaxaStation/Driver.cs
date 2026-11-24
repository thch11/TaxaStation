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

        public void AddWorkTime(int minutes)
        {
            WorkTime = WorkTime + minutes;
        }
        public override string ToString()
        {
            return Name + " " + " | " + Availability + " | " + WorkStart + " - " + WorkEnd;
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

    }
}
