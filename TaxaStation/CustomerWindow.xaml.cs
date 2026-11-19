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

namespace TaxaStation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        TaxiStation s;
        public CustomerWindow(TaxiStation station)
        {
            s = station;
            InitializeComponent();
            List<Driver> AvailableDrivers = new List<Driver>();
            int i = 0;
            while(i < s.DriverList.Count)
            {
                if (s.DriverList[i].GetAvailability() == "Available")
                {
                    AvailableDrivers.Add(s.DriverList[i]);
                }
                i++;
            }
            DriverListBoxCustomer.ItemsSource = AvailableDrivers;
            this.Closed += new EventHandler(CustomerWindow_Closed);
        }
        private void Book_Taxi_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DriverListBoxCustomer.SelectedItem != null)
            {
                Driver d = (Driver)DriverListBoxCustomer.SelectedItem;
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
            string filepath = "JSON/Save.json";
            string json = JsonSerializer.Serialize(s.DriverList);
            System.IO.File.WriteAllText(filepath, json);
        }
    }
}
