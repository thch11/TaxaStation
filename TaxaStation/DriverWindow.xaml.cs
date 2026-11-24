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
    public partial class DriverWindow : Window
    {
        TaxiStation s;

        User currentUser;

        Driver currentDriver;
        public DriverWindow(TaxiStation station, User loginUser)
        {
            s = station;
            currentUser = loginUser;
            int i = 0;
            currentDriver = s.FindDriver(currentUser);
            InitializeComponent();
            DriverListBoxDriver.ItemsSource = s.DriverList;
            this.Closed += new EventHandler(DriverWindow_Closed);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }

        private void UnavilableButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Unavailable");
            DriverListBoxDriver.Items.Refresh();
        }

        private void AvailableButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Available");
            DriverListBoxDriver.Items.Refresh();
        }

        private void OnBreakButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("On Break");
            DriverListBoxDriver.Items.Refresh();
        }

        private void NotWorkingButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Not Working");
            DriverListBoxDriver.Items.Refresh();
        }
        void DriverWindow_Closed(object? sender, EventArgs e)
        {
            s.SaveToFile();
        }
    }
}
