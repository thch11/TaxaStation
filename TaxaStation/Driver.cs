using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxaStation
{
    public class Driver
    {
        [JsonInclude]
        private string Name { get; set; }
        [JsonInclude]
        private int WorkStart { get; set; }
        [JsonInclude]
        private int WorkEnd { get; set; }
        [JsonInclude]
        private string Availability { get; set; } = "Available";

        public Driver(string name, int workstart, int workend)
        {
            Name = name;
            WorkStart = workstart;
            WorkEnd = workend;
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
