using Syncfusion.UI.Xaml.Grid.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VendingMachines.pages
{
    /// <summary>
    /// Логика взаимодействия для Otchet.xaml
    /// </summary>
    public partial class Otchet : Page
    {
        public Otchet()
        {
            InitializeComponent();
            VndEntities database = new VndEntities();
            reportGrid.ItemsSource = database.Report.ToList();

            actuallyData.Content = DateTime.Today;
        }

        private void reportGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void savePdf_Click(object sender, RoutedEventArgs e)
        {
     
        }
    }
}
