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
    /// Логика взаимодействия для DrinkRedact.xaml
    /// </summary>
    public partial class DrinkRedact : Page
    {
        int drkId;
        int drinkCount;
        public DrinkRedact(int VendingMachineId, int drinkId, string drinkName, int drinkCost, string drinkPicture)
        {
            InitializeComponent();
            lblName.Content = drinkName;
            txtCost.Text = drinkCost.ToString();
            drkId = drinkId;

            VndEntities database = new VndEntities();
            var u = database.VendingMachineDrinks.Single(a => a.DrinksId == drinkId);
            var id = u.Id;
            drinkCount = u.Count;
            txtCount.Text = Convert.ToString(u.Count);
        }

        private void PicLoad(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VndEntities database = new VndEntities();
            var reportdrk = database.Report.Single(a => a.DrinkId == drkId);
            reportdrk.AfterUpdate = drinkCount;

            var vnddrk = database.VendingMachineDrinks.Single(a => a.DrinksId == drkId);
            vnddrk.Count = Convert.ToInt32(txtCount.Text);

            var drk = database.Drinks.Single(a => a.Id == drkId);
            drk.Cost = Convert.ToInt32(txtCost.Text);    
            


            MessageBox.Show("Данные успешно изменены");

            database.SaveChanges();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            VndEntities database = new VndEntities();
            var drk = database.Drinks.Single(a => a.Id == drkId);
            database.Drinks.Remove(drk);

            var vnddrk = database.VendingMachineDrinks.Single(a => a.DrinksId == drkId);
            database.VendingMachineDrinks.Remove(vnddrk);

            var rptDrink = database.Report.Single(a => a.DrinkId == drkId);
            database.VendingMachineDrinks.Remove(vnddrk);

            MessageBox.Show("Напиток был удален");

            database.SaveChanges();
        }
    }
}
