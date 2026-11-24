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
   
    public partial class OwnerWindow : Window
    {
        TaxiStation s;

        User currentUser;
        private DispatcherTimer _timer;

        public OwnerWindow(TaxiStation station, User loginUser)
        {
            s = station;
            currentUser = loginUser;
            InitializeComponent();
            RefreshUI();
            this.Closed += new EventHandler(OwnerWindow_Closed);

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
        private void RefreshUI()
        {
            CarComboBox.ItemsSource = s.GetAvailableCarShifts();
            DriverListBoxOwner.ItemsSource = s.GetCarStatuses(DateTime.Now.Hour);
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            if (DriverTextBoxInput.Text != "" &&
                int.TryParse(WorkingHoursEndTextBoxInput.Text, out int resultEnd) &&
                int.TryParse(WorkingHoursStartTextBoxInput.Text, out int resultStart) &&
                DriverTextBoxInputPassword.Text != "" &&
                CarComboBox.SelectedItem != null)
            {
                if (resultEnd >= 0 && resultEnd <= 24 && resultStart >= 0 && resultStart <= 24)
                {
                    Car selectedCar = ((CarShiftOption)CarComboBox.SelectedItem).Car;

                    bool success = s.TryAssignDriver(DriverTextBoxInput.Text, DriverTextBoxInputPassword.Text, resultStart, resultEnd, selectedCar);

                    if (success)
                    {
                        RefreshUI();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Invalid shift (must be 0-8, 8-16, or 16-24).", "MuteMotion");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid work hours, values must be from 0 to 24", "MuteMotion");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid input work hours must be values from 0 to 24, and name & password can not be empty, and you must select a car", "MuteMotion");
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
            if (DriverListBoxOwner.SelectedItem != null)
            {
                ListBoxItem selectedItem = (ListBoxItem)DriverListBoxOwner.SelectedItem;

                if (selectedItem.Tag != null)
                {
                    Driver selectedDriver = (Driver)selectedItem.Tag;
                    s.RemoveDriverAndFreeCar(selectedDriver);
                    RefreshUI();
                }
                else
                {
                    System.Windows.MessageBox.Show("This car currently has no active driver to remove.", "MuteMotion");
                }
            }
        }
        

        private void AddCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarTextBoxCarBrand.Text != "" && CarTextBoxLicensePlate.Text != "" && CarTextBoxKMDriven.Text != "")
            {
                s.AddCar(CarTextBoxCarBrand.Text, CarTextBoxLicensePlate.Text, Convert.ToInt32(CarTextBoxKMDriven.Text));
                RefreshUI();
            }
            else
                System.Windows.MessageBox.Show("No empty fields allowed when adding a car", "MuteMotion");
        }
    }
}
