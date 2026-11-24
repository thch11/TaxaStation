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
using System.Text.Json;
using System.IO;

namespace TaxaStation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        TaxiStation s;

        User currentUser;
        public OwnerWindow(TaxiStation station, User loginUser)
        {
            s = station;
            currentUser = loginUser;
            InitializeComponent();
            DriverListBoxOwner.ItemsSource = s.DriverList;
            this.Closed += new EventHandler(OwnerWindow_Closed);
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DriverTextBoxInput.Text != "" && int.TryParse(WorkingHoursEndTextBoxInput.Text, out int resultEnd) && int.TryParse(WorkingHoursStartTextBoxInput.Text, out int resultStart) && DriverTextBoxInputPassword.Text != "")
            {
                if (resultEnd >= 0 && resultEnd <= 24 && resultStart >= 0 && resultStart <= 24)
                {
                    s.AddDriver(DriverTextBoxInput.Text, Convert.ToInt32(WorkingHoursStartTextBoxInput.Text), Convert.ToInt32(WorkingHoursEndTextBoxInput.Text));
                    DriverListBoxOwner.ItemsSource = s.DriverList;
                    DriverListBoxOwner.Items.Refresh();

                    s.AddUser(DriverTextBoxInput.Text, DriverTextBoxInputPassword.Text);
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid work hours, values must be from 0 to 24", "MuteMotion");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid input work hours must be values from 0 to 24, and name & password can not be empty", "MuteMotion");
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }
        void OwnerWindow_Closed(object? sender, EventArgs e)
        {
            s.SaveToFile();
        }

        private void Remove_Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            s.RemoveDriver((Driver)DriverListBoxOwner.SelectedItem);
            DriverListBoxOwner.Items.Refresh();
        }
    }
}
