using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TaxaStation
{
    public partial class DriverWindow : Window
    {
        TaxiStation s;

        User currentUser;

        Driver currentDriver;
        private DispatcherTimer _timer;

        public DriverWindow(TaxiStation station, User loginUser)
        {
            s = station;
            currentUser = loginUser;
            currentDriver = s.FindDriver(currentUser);
            InitializeComponent();
            NameLabel.Content = "Welcome " + currentDriver.Name;
            WorkHoursLabel.Content = "From: " + currentDriver.WorkStart + " to: " + currentDriver.WorkEnd;
            DriversCarLabel.Content =  currentDriver.driversCar.CarBrand + " " + currentDriver.driversCar.LicensePlate;
            ActiveTimeLabel.Content = "Hours: " + currentDriver.WorkTime / 60 + Environment.NewLine +  "Minutes: " + currentDriver.WorkTime % 60;
            DriverListBoxOwner.ItemsSource = s.GetCarStatuses(DateTime.Now.Hour);
            this.Closed += new EventHandler(DriverWindow_Closed);
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1),
            };

            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            myClockLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }

        private void UnavilableButton_Click(object sender, RoutedEventArgs e)
        {
            if (!s.CheckIfDriverBooked(currentDriver))
            {
                s.AddLog(currentDriver, DateTime.Now.ToString("HH:mm:ss"), "Unavailable");
                currentDriver.ChangeAvailability("Unavailable");
            }
            else
                System.Windows.MessageBox.Show("You are currently booked");
        }

        private void AvailableButton_Click(object sender, RoutedEventArgs e)
        {
            if (s.CheckIfDriverBooked(currentDriver))
            {
                s.AddMinutesToDriver(s.FindBookedLog(currentDriver), currentDriver);
                s.AddLog(currentDriver, DateTime.Now.ToString("HH:mm:ss"), "Drive finished");
                currentDriver.ChangeAvailability("Available");
                ActiveTimeLabel.Content = "Hours: " + currentDriver.WorkTime / 60 + Environment.NewLine + "Minutes: " + currentDriver.WorkTime % 60;
                DriverListBoxOwner.ItemsSource = s.GetCarStatuses(DateTime.Now.Hour);
            }
            else if(currentDriver.GetAvailability() == "On Break" || currentDriver.GetAvailability() == "Not Working")
            {
                currentDriver.ChangeAvailability("Available");
            }
            else    
                System.Windows.MessageBox.Show("You are not currently booked");
        }

        private void OnBreakButton_Click(object sender, RoutedEventArgs e)
        {
            s.AddLog(currentDriver, DateTime.Now.ToString("HH:mm:ss"), "On Break");
            currentDriver.ChangeAvailability("On Break");
        }

        private void NotWorkingButton_Click(object sender, RoutedEventArgs e)
        {
            s.AddLog(currentDriver, DateTime.Now.ToString("HH:mm:ss"), "Not Working");
            currentDriver.ChangeAvailability("Not Working");
        }
        void DriverWindow_Closed(object? sender, EventArgs e)
        {
            s.SaveToFile();
        }
    }
}
