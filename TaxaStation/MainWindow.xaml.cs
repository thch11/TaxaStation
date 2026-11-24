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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaxaStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaxiStation s;

        public MainWindow(TaxiStation station)
        {
            s = station;
            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
        }

        //private void Owner_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OwnerWindow ownerWindow = new OwnerWindow(s);
        //    ownerWindow.Show();
        //    this.Close();
        //}

        private void Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow secondWindow = new CustomerWindow(s);
            secondWindow.Show();
            this.Close();
        }

        //private void Driver_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    DriverWindow driverWindow = new DriverWindow(s);
        //    driverWindow.Show();
        //    this.Close();
        //}
        void MainWindow_Closed(object? sender, EventArgs e)
        {
            s.SaveToFile();
        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while(s.userList.Count > i)
            {
                if (s.userList[i].GetName() == TextBoxUsername.Text && s.userList[i].GetPassword() == TextBoxPassword.Text && s.userList[i].AdminAccount() == true)
                {
                    OwnerWindow ownerWindow = new OwnerWindow(s, s.userList[i]);
                    ownerWindow.Show();
                    this.Close();
                    break;
                }
                else if (s.userList[i].GetName() == TextBoxUsername.Text && s.userList[i].GetPassword() == TextBoxPassword.Text)
                {
                    DriverWindow driverWindow = new DriverWindow(s, s.userList[i]);
                    driverWindow.Show();
                    this.Close();
                    break;
                }
                i++;
                if (i == s.userList.Count)
                    System.Windows.MessageBox.Show("Invalid credentials, try again", "MuteMotion");
            }
        }
    }
}