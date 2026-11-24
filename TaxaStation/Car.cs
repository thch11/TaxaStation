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
        [JsonInclude]
        public bool MidnightToEight { get; private set; } = false;
        [JsonInclude]
        public bool EightToSixteen { get; private set; } = false;
        [JsonInclude]
        public bool SixteenToMidnight { get; private set; } = false;

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

        public void BookMidnightToEight()
        {
            this.MidnightToEight = true;
        }
        public void BookEightToSixteen()
        {
            this.EightToSixteen = true;
        }
        public void BookSixteenToMidnight()
        {
            this.SixteenToMidnight = true;
        }
        public void UnbookMidnightToEight()
        {
            this.MidnightToEight = false;
        }
        public void UnbookEightToSixteen()
        {
            this.EightToSixteen = false;
        }
        public void UnbookSixteenToMidnight()
        {
            this.SixteenToMidnight = false;
        }

        public bool IsShiftAvailable(int startHour, int endHour)
        {
            if (startHour == 0 && endHour == 8) 
                return !MidnightToEight;
            if (startHour == 8 && endHour == 16) return 
                    !EightToSixteen;
            if (startHour == 16 && endHour == 24) return 
                    !SixteenToMidnight;
            return false;
        }

        public bool TryBookShift(int startHour, int endHour)
        {
            if (startHour == 0 && endHour == 8 && !MidnightToEight)
            {
                MidnightToEight = true;
                return true;
            }
            if (startHour == 8 && endHour == 16 && !EightToSixteen)
            {
                EightToSixteen = true;
                return true;
            }
            if (startHour == 16 && endHour == 24 && !SixteenToMidnight)
            {
                SixteenToMidnight = true;
                return true;
            }
            return false;
        }

        public void UnbookShift(int startHour, int endHour)
        {
            if (startHour == 0 && endHour == 8) 
                MidnightToEight = false;
            else if (startHour == 8 && endHour == 16) 
                EightToSixteen = false;
            else if (startHour == 16 && endHour == 24) 
                SixteenToMidnight = false;
        }

    }
}
