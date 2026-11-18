using System.Configuration;
using System.Data;
using System.Windows;
using System.Text.Json;

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

            string filepath = "JSON/Save.json";
            try
            {
                masterStation.DriverList = JsonSerializer.Deserialize<List<Driver>>(System.IO.File.ReadAllText(filepath));
            }
            catch (Exception ex) { }

            MainWindow mainWindow = new MainWindow(masterStation);
            mainWindow.Show();
        }
    }

}
