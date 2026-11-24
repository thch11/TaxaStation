using System.Configuration;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace TaxaStation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            TaxiStation masterStation = new TaxiStation();

            string projectDirectory = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            string folderPath = Path.Combine(projectDirectory, "Login");
            string filepath = Path.Combine(folderPath, "Login.json");
            
            masterStation.userList = JsonSerializer.Deserialize<List<User>>(System.IO.File.ReadAllText(filepath));

            folderPath = Path.Combine(projectDirectory, "JSON");
            filepath = Path.Combine(folderPath, "Save.json");
            
            masterStation.DriverList = JsonSerializer.Deserialize<List<Driver>>(System.IO.File.ReadAllText(filepath));
            

            MainWindow mainWindow = new MainWindow(masterStation);
            mainWindow.Show();
        }
    }

}
