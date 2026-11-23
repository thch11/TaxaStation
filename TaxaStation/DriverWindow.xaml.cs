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

        List<User> userList;

        User currentUser;

        Driver currentDriver;
        public DriverWindow(TaxiStation station, List<User> uList, User loginUser)
        {
            userList = uList;
            s = station;
            currentUser = loginUser;
            int i = 0;
            while (i < s.DriverList.Count)
            {
                if (currentUser.GetName() == s.DriverList[i].GetName())
                {
                    currentDriver = s.DriverList[i];
                    break;
                }
                    i++;
            }
            InitializeComponent();
            DriverListBoxDriver.ItemsSource = s.DriverList;
            this.Closed += new EventHandler(DriverWindow_Closed);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s, userList);
            mainWindow.Show();
            this.Close();
        }

        private void UnavilableButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Unavailable");
            DriverListBoxDriver.Items.Refresh();
            //if (DriverListBoxDriver.SelectedItem != null)
            //{
            //    Driver d = (Driver)DriverListBoxDriver.SelectedItem;
            //    d.ChangeAvailability("Unavailable");
            //    DriverListBoxDriver.Items.Refresh();
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Please select a driver from the driver list!", "MuteMotion");
            //}
        }

        private void AvailableButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Available");
            DriverListBoxDriver.Items.Refresh();
            //if (DriverListBoxDriver.SelectedItem != null)
            //{
            //    Driver d = (Driver)DriverListBoxDriver.SelectedItem;
            //    d.ChangeAvailability("Available");
            //    DriverListBoxDriver.Items.Refresh();
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Please select a driver from the driver list!", "MuteMotion");
            //}
        }

        private void OnBreakButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("On Break");
            DriverListBoxDriver.Items.Refresh();
            //if (DriverListBoxDriver.SelectedItem != null)
            //{
            //    Driver d = (Driver)DriverListBoxDriver.SelectedItem;
            //    d.ChangeAvailability("On break");
            //    DriverListBoxDriver.Items.Refresh();
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Please select a driver from the driver list!", "MuteMotion");
            //}
        }

        private void NotWorkingButton_Click(object sender, RoutedEventArgs e)
        {
            currentDriver.ChangeAvailability("Not Working");
            DriverListBoxDriver.Items.Refresh();
            //if (DriverListBoxDriver.SelectedItem != null)
            //{
            //    Driver d = (Driver)DriverListBoxDriver.SelectedItem;
            //    d.ChangeAvailability("Not working");
            //    DriverListBoxDriver.Items.Refresh();
            //}
            //else
            //{
            //    System.Windows.MessageBox.Show("Please select a driver from the driver list!", "MuteMotion");
            //}
        }
        void DriverWindow_Closed(object? sender, EventArgs e)
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "JSON");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Save.json");
            string json = JsonSerializer.Serialize(s.DriverList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);
        }
    }
}
