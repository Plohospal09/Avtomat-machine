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
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Page
    {
        public Password()
        {
            InitializeComponent();
        }

       

        private void AdminJoin(object sender, RoutedEventArgs e)
        {
            try
            {
                VndEntities database = new VndEntities();
                var u = database.VendingMachines.Single(a => a.SecretCode.ToString() == txtSecret.Password);
                MainAdmin newFonm = new MainAdmin();
                newFonm.VendinMachineId = u.Id;
                newFonm.Show();
                btnAdmin.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("Неверный секретный код");
            }                 
        }
    }
}
