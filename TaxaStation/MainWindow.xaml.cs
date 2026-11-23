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

        List<User> userList;
        public MainWindow(TaxiStation station, List<User> uList)
        {
            userList = uList;
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
            CustomerWindow secondWindow = new CustomerWindow(s, userList);
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
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "JSON");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Save.json");
            string json = JsonSerializer.Serialize(s.DriverList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);
        }
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            while(userList.Count > i)
            {
                if (userList[i].GetName() == TextBoxUsername.Text && userList[i].GetPassword() == TextBoxPassword.Text && userList[i].AdminAccount() == true)
                {
                    OwnerWindow ownerWindow = new OwnerWindow(s, userList, userList[i]);
                    ownerWindow.Show();
                    this.Close();
                    break;
                }
                else if (userList[i].GetName() == TextBoxUsername.Text && userList[i].GetPassword() == TextBoxPassword.Text)
                {
                    DriverWindow driverWindow = new DriverWindow(s, userList, userList[i]);
                    driverWindow.Show();
                    this.Close();
                    break;
                }
                i++;
                if (i == userList.Count)
                    System.Windows.MessageBox.Show("Invalid credentials, try again", "MuteMotion");
            }
        }
    }
}