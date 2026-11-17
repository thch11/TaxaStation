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
        public OwnerWindow()
        {
            InitializeComponent();
        }
        //her er tekst
        private void AddDriverButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
