using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TaxaStation
{
    public class TaxiStation
    {
        public List<Driver> DriverList { get; set; } = new List<Driver>();

        public List<User> userList { get; set; } = new List<User>();

        public List<Car> carList { get; set; } = new List<Car>();

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

        public void SaveToFile()
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "JSON");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Save.json");
            string json = JsonSerializer.Serialize(DriverList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);

            folderPath = System.IO.Path.Combine(projectDirectory, "Login");

            Directory.CreateDirectory(folderPath);

            filepath = System.IO.Path.Combine(folderPath, "Login.json");
            json = JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);
        }

        public List<Driver> FindAvailableDrivers()
        {
            List<Driver> newList = new List<Driver>();
            int i = 0;
            while (i < DriverList.Count)
            {
                if (DriverList[i].GetAvailability() == "Available")
                {
                    newList.Add(DriverList[i]);
                }
                i++;
            }
            return newList;
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

        public void LogTime(string time, string name, string action)
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "LogFiles");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Log.txt");
            string logString = name + " " + time + " " + action + Environment.NewLine; 
            System.IO.File.AppendAllText(filepath, logString);
        }
    }
}
