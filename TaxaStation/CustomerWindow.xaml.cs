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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        TaxiStation s;

        private DispatcherTimer _timer;

        public CustomerWindow(TaxiStation station)
        {
            s = station;
            InitializeComponent();
            List<Driver> AvailableDrivers = s.FindAvailableDrivers();
            DriverListBoxCustomer.ItemsSource = AvailableDrivers;
            this.Closed += new EventHandler(CustomerWindow_Closed);

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
        private void Book_Taxi_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DriverListBoxCustomer.SelectedItem != null)
            {
                Driver d = (Driver)DriverListBoxCustomer.SelectedItem;
                s.LogTime(DateTime.Now.ToString("HH:mm:ss"), d.GetName(), "Booked");
                d.ChangeAvailability("Unavailable");
                System.Windows.MessageBox.Show("Thank you for booking a taxi with MuteMotion", "MuteMotion");
                MainWindow mainWindow = new MainWindow(s);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("You must select an available driver from the driver list!", "MuteMotion");
            }

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }
        void CustomerWindow_Closed(object? sender, EventArgs e)
        {
            s.SaveToFile();
        }
    }
}
