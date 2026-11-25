using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;

namespace TaxaStation
{
    public class TaxiStation
    {
        public List<Driver> DriverList { get; set; } = new List<Driver>();

        public List<User> userList { get; set; } = new List<User>();

        public List<Car> carList { get; set; } = new List<Car>();

        public List<Log> logList { get; set; } = new List<Log>();

        public Car AddCar(string brand, string licenseplate, int KMdriven)
        {
            Car car = AddCarToList(CreateCar(brand, licenseplate, KMdriven));
            return car;
        }
        private Car CreateCar(string brand, string licenseplate, int KMdriven)
        {
            Car car = new Car(brand, licenseplate, KMdriven);
            return car;
        }
        private Car AddCarToList(Car car)
        {
            carList.Add(car);
            return car;
        }
        public Driver AddDriver(string name, int workstart, int workend, Car car)
        {
           Driver driver = AddDriverToList(CreateDriver(name, workstart, workend, car));
           return driver;
        }
        private Driver CreateDriver(string name, int workstart, int workend, Car car)
        {
            Driver driver = new Driver(name, workstart, workend, car);
            return driver;
        }
        private Driver AddDriverToList(Driver driver)
        {
            DriverList.Add(driver);
            return driver;
        }
        public User AddUser(string name, string password)
        {
            User user = AddUserToList(CreateUser(name, password));
            return user;
        }
        private User CreateUser(string name, string password)
        {
            User user = new User(name, password);
            return user;
        }
        private User AddUserToList(User user)
        {
            userList.Add(user);
            return user;

        }
        public List<ListBoxItem> ConvertDriverToListBoxItem(List<Driver> drivList)
        {
            int i = 0;
            List<ListBoxItem> itemList = new List<ListBoxItem>();
            while(i < drivList.Count)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = drivList[i].Name + " | " + drivList[i].GetAvailability();
                item.Tag = drivList[i];
                itemList.Add(item);
                i++;
            }
            return itemList;
        }
        public void SaveToFile()
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "JSON");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Save.json");
            string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);
        }

        public List<ListBoxItem> FindAvailableDrivers()
        {
            List<Driver> newList = new List<Driver>();
            int i = 0;
            while (i < DriverList.Count)
            {
                if (DriverList[i].GetAvailability() == "Available" && (DateTime.Now.Hour < DriverList[i].WorkEnd && DateTime.Now.Hour >= DriverList[i].WorkStart))
                {
                    newList.Add(DriverList[i]);
                }
                i++;
            }
            List<ListBoxItem> returnList = ConvertDriverToListBoxItem(newList);
            return returnList;
        }

        public Driver FindDriver(User user)
        {
            int i = 0;
            while (i < DriverList.Count)
            {
                if (user.GetName() == DriverList[i].GetName())
                {
                    return DriverList[i];
                }
                i++;
            }
            return null;
        }

        public void RemoveDriver(Driver driver)
        {
            User selectedUser = null;
            Driver selectedDriver = driver;
            int i = 0;
            while (i < userList.Count)
            {
                if (selectedDriver.GetName() == userList[i].GetName())
                {
                    selectedUser = userList[i];
                }
                i++;
            }
            DriverList.Remove(selectedDriver);
            userList.Remove(selectedUser);
        }

        public List<CarShiftOption> GetAvailableCarShifts()
        {
            List<CarShiftOption> availableCars = new List<CarShiftOption>();
            int i = 0;
            while (i < carList.Count)
            {
                if (!carList[i].MidnightToEight)
                {
                    availableCars.Add(new CarShiftOption { Car = carList[i], StartHour = 0, EndHour = 8 });
                }
                if (!carList[i].EightToSixteen)
                {
                    availableCars.Add(new CarShiftOption { Car = carList[i], StartHour = 8, EndHour = 16 });
                }
                if (!carList[i].SixteenToMidnight)
                {
                    availableCars.Add(new CarShiftOption { Car = carList[i], StartHour = 16, EndHour = 24 });
                }
                i++;
            }
            return availableCars;
        }
        public bool AssignCarToDriver(Driver driver, CarShiftOption shiftOption)
        {
            if (driver == null || shiftOption == null || shiftOption.Car == null)
            {
                return false;
            }
            if (driver.driversCar != null)
            {
                return false;
            }
            if (!shiftOption.Car.BookShift(shiftOption.StartHour, shiftOption.EndHour))
            {
                return false;
            }
            driver.AssignCarAndShift(shiftOption.Car, shiftOption.StartHour, shiftOption.EndHour);
            return true;
        }
        public List<ListBoxItem> GetCarStatuses(int currentHour)
        {
            List<ListBoxItem> carStatusList = new List<ListBoxItem>();
            int i = 0;
            while (i < carList.Count)
            {
                Car currentCar = carList[i];
                string currentDriverName = "Available";
                Driver activeDriver = null;

                int j = 0;
                while (j < DriverList.Count)
                {
                    Driver currentDriver = DriverList[j];

                    if (currentDriver.driversCar != null && currentDriver.driversCar.LicensePlate == currentCar.LicensePlate)
                    {
                        if (currentHour >= currentDriver.WorkStart && currentHour < currentDriver.WorkEnd)
                        {
                            currentDriverName = currentDriver.Name;
                            activeDriver = currentDriver;
                            break;
                        }
                    }
                    j++;
                }
                ListBoxItem item = new ListBoxItem();
                if (activeDriver != null)
                {
                    item.Content = currentCar.CarBrand + " | " + currentCar.LicensePlate + " | " + currentDriverName + " | " + activeDriver.GetAvailability();
                }
                else
                {
                    item.Content = currentCar.CarBrand + " | " + currentCar.LicensePlate + " | " + currentDriverName;
                }
                item.Tag = activeDriver; 

                carStatusList.Add(item);
                i++;
            }
            return carStatusList;
        }

        public bool AssignDriver(string name, string password, int startHour, int endHour, Car selectedCar)
        {
            if (!selectedCar.BookShift(startHour, endHour))
            {
                return false;
            }

            AddDriver(name, startHour, endHour, selectedCar);
            AddUser(name, password);
            return true;
        }

        public void RemoveDriverFromCar(Driver driverToRemove)
        {
            if (driverToRemove.driversCar != null)
            {
                int i = 0;
                while (i < carList.Count)
                {
                    if (carList[i].LicensePlate == driverToRemove.driversCar.LicensePlate)
                    {
                        carList[i].UnbookShift(driverToRemove.WorkStart, driverToRemove.WorkEnd);
                        driverToRemove.RemoveCar();
                        break;
                    }
                    i++;
                }
            }
        }

        public Log AddLog(Driver driver, string time, string action)
        {
            Log log = AddLogToList(CreateLog(driver, time, action));
            return log;
        }
        private Log CreateLog(Driver driver, string time, string action)
        {
            Log log = new Log(driver, time, action);
            return log;
        }
        private Log AddLogToList(Log log)
        {
            logList.Add(log);
            return log;
        }

        public bool CheckIfDriverBooked(Driver driver)
        {
            int i = 0; 
            while(i < logList.Count)
            {
                if(driver.Name == logList[i].Driver.Name && logList[i].Action == "Booked")
                {
                    return true;
                }
                i++;
            }
            return false;
        }

        public Log FindBookedLog(Driver driver)
        {
            int i = 0;
            while (i < logList.Count)
            {
                if (driver.Name == logList[i].Driver.Name && logList[i].Action == "Booked")
                {
                    return logList[i];
                }
                i++;
            }
            return null;
        }
        
        public void AddMinutesToDriver(Log log, Driver driver)
        {
            string startString = log.Time;

            DateTime start = DateTime.Parse(startString);
            DateTime end = DateTime.Now;

            if (end < start)
            {
                end = end.AddDays(1);
            }

            double totalMinutes = (end - start).TotalMinutes;

            driver.AddMinutes(Convert.ToInt32(totalMinutes));

            RemoveLogFromList(log);
        }

        public void RemoveLogFromList(Log log)
        {
            logList.Remove(log);
        }
    }
}
