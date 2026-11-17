using System.Configuration;
using System.Data;
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

            MainWindow mainWindow = new MainWindow(masterStation);
            mainWindow.Show();
        }
    }

}
