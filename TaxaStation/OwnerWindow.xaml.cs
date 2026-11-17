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
    public partial class OwnerWindow : Window
    {
        TaxiStation s;
        public OwnerWindow(TaxiStation station)
        {
            s = station;
            InitializeComponent();
            DriverListBoxOwner.ItemsSource = s.DriverList;
        }

        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {
            s.AddDriver(DriverTextBoxInput.Text, Convert.ToInt32(WorkingHoursStartTextBoxInput.Text), Convert.ToInt32(WorkingHoursEndTextBoxInput.Text));
            DriverListBoxOwner.ItemsSource = s.DriverList;
            DriverListBoxOwner.Items.Refresh();
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(s);
            mainWindow.Show();
            this.Close();
        }
    }
}
