using System;
using System.Collections.Generic;
using System.Text;

namespace TaxaStation
{
    public class Driver
    {
        private string Name;
        private int WorkStart;
        private int WorkEnd;

        private string Availability = "Available";

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

    }
}
