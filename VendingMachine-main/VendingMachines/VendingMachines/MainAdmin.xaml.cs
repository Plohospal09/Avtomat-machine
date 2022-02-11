using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VendingMachines
{
    /// <summary>
    /// Логика взаимодействия для MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        public int VendinMachineId;
        public MainAdmin()
        {
            InitializeComponent();
        }

        private void btnCoins(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.Navigate(new pages.Coins());

        }

        private void btnDrinks(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.Navigate(new pageDrink(VendingMachineId:VendinMachineId));
        }

        private void btnOtchet(object sender, RoutedEventArgs e)
        {
           Frame.NavigationService.Navigate(new pages.Otchet());
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow newForm = new MainWindow();
            this.Hide();
        }
    }
}
