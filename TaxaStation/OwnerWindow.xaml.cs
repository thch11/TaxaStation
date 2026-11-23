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

        List<User> userList;

        User currentUser;
        public OwnerWindow(TaxiStation station, List<User> uList, User loginUser)
        {
            userList = uList;
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

                    User newUser = new User(DriverTextBoxInput.Text, DriverTextBoxInputPassword.Text);
                    userList.Add(newUser);
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
            MainWindow mainWindow = new MainWindow(s, userList);
            mainWindow.Show();
            this.Close();
        }
        void OwnerWindow_Closed(object? sender, EventArgs e)
        {
            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = System.IO.Path.Combine(projectDirectory, "JSON");

            Directory.CreateDirectory(folderPath);

            string filepath = System.IO.Path.Combine(folderPath, "Save.json");
            string json = JsonSerializer.Serialize(s.DriverList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);

            folderPath = System.IO.Path.Combine(projectDirectory, "Login");

            Directory.CreateDirectory(folderPath);

            filepath = System.IO.Path.Combine(folderPath, "Login.json");
            json = JsonSerializer.Serialize(userList, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filepath, json);
        }

        private void Remove_Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = null;
            Driver selectedDriver = (Driver)DriverListBoxOwner.SelectedItem;
            int i = 0; 
            while(i < userList.Count)
            {
                if(selectedDriver.GetName() == userList[i].GetName())
                {
                    selectedUser = userList[i];
                }
                i++;
            }
            try
            {
                s.DriverList.Remove(selectedDriver);
                userList.Remove(selectedUser);
                DriverListBoxOwner.Items.Refresh();
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show("User doesnt exist in database!", "MuteMotion");
            }
        }
    }
}
