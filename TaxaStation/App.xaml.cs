using System.Configuration;
using System.Data;
using System.Diagnostics;
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

            HashSet<Car> CarCollection = new HashSet<Car>();
            int i = 0; 
            while(i < masterStation.DriverList.Count)
            {
                CarCollection.Add(masterStation.DriverList[i].driversCar);
                i++;
            }

            masterStation.carList = CarCollection.DistinctBy(c => c.LicensePlate).ToList();

            MainWindow mainWindow = new MainWindow(masterStation);
            mainWindow.Show();
        }
    }

}
