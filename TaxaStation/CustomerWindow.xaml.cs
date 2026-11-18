using System;
using System.Collections.Generic;
using System.Text;
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
        }
        private void Book_Taxi_Button_Click(object sender, RoutedEventArgs e)
        {
            Driver d = (Driver)DriverListBoxCustomer.SelectedItem;
            d.ChangeAvailability("Unavailable");
            System.Windows.MessageBox.Show("Tak fordi du bookede en taxi hos MuteMotion", "MuteMotion");
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }
    }
}
