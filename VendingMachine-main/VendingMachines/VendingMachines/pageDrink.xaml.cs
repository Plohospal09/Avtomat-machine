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
using System.Windows.Threading;

namespace VendingMachines
{
    /// <summary>
    /// Логика взаимодействия для pageDrink.xaml
    /// </summary>
    public partial class pageDrink : Page
    {
        int vndId;
        public pageDrink(int VendingMachineId)
        {
            InitializeComponent();
            Frame.NavigationService.Navigate(new pages.newDrink(VendingMachineId));
            vndId = VendingMachineId;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();
        }

        public void UpdateData(object sender, object e)
        {
            VndEntities database = new VndEntities();
            lstDrink.ItemsSource = database.Drinks.ToList();         
        }
        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void lstDrink_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstDrink.SelectedItem != null)
            {
                string drinkName = (lstDrink.SelectedItem as Drinks).Name;
                string drinkPicture = (lstDrink.SelectedItem as Drinks).Image;
                int drinkCost = Convert.ToInt32((lstDrink.SelectedItem as Drinks).Cost);
                int drinkId = (lstDrink.SelectedItem as Drinks).Id;
                Frame.NavigationService.Navigate(new pages.DrinkRedact(vndId, drinkId, drinkName, drinkCost, drinkPicture));
            }
            lstDrink.SelectedItem = null;
        }
    }
}
