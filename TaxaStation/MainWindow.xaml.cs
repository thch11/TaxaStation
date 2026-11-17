using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Owner_Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerWindow ownerWindow = new OwnerWindow();
            ownerWindow.Show();
            this.Close();
        }

        private void Customer_Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow secondWindow = new CustomerWindow();
            secondWindow.Show();
            this.Close();
        }

        private void Driver_Button_Click(object sender, RoutedEventArgs e)
        {
            DriverWindow driverWindow = new DriverWindow();
            driverWindow.Show();
            this.Close();
        }
    }
}