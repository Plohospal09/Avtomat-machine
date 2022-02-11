using Microsoft.Win32;
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
using System.IO;

namespace VendingMachines.pages
{
    /// <summary>
    /// Логика взаимодействия для newDrink.xaml
    /// </summary>
    public partial class newDrink : Page
    {
        int venid;
        public newDrink(int VendingMachineId)
        {
            InitializeComponent();
            venid = VendingMachineId;
        }

        private void PicLoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf *.jpeg";

            if (openDialog.ShowDialog() == true)
            {
                picpath.Text = System.IO.Path.GetFileName(openDialog.FileName);
                Drinkpic.Source = new BitmapImage(new Uri(openDialog.FileName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VndEntities database = new VndEntities();
            Drinks drinkadd = new Drinks { Name = DrinkName.Text, Image = "drinks/" + picpath.Text, Cost = Convert.ToDecimal(DrinkCost.Text) };
            database.Drinks.Add(drinkadd);
            int drkid = database.Drinks.Max(id => id.Id);
            VendingMachineDrinks drinkcount = new VendingMachineDrinks { VendingMachineId = venid, DrinksId = drkid + 1, Count = Convert.ToInt32(DrinkCount.Text) };

            database.VendingMachineDrinks.Add(drinkcount);

            Report reportadd = new Report { VendingMachineId = venid, DrinkId = drkid + 1, AfterUpdate = Convert.ToInt32(DrinkCount.Text), Profit = 0 };
            database.Report.Add(reportadd);

            database.SaveChanges();
            MessageBox.Show("Напиток успешно добавлен.");
        }
    }
}
