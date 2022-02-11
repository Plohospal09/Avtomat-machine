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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int actuallyCoin = 0;
        public int[] coins_add = new int[4];

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateData;
            timer.Start();                             
        }

        public void UpdateData(object sender, object e)
        {
            VndEntities database = new VndEntities();
            lstDrink.ItemsSource = database.Drinks.ToList();
            List<Button> CoinButtons = new List<Button>() { NomOne, NomTwo, NomFive, NomTen };
            for (int i = 0; i < CoinButtons.Count; i++)
            {
                var coindem = CoinButtons[i].Content;
                var u = database.Coins.Single(a => a.Denomination.ToString() == coindem.ToString());
                var id = u.Id;

                var q = database.VendingMachineCoins.Single(d => d.CoinsId == id);
                var active = q.IsActive;
                if (active == 0)
                {
                    CoinButtons[i].IsEnabled = false;
                }
                else
                {
                    CoinButtons[i].IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn5(object sender, RoutedEventArgs e)
        {
            actuallyCoin += 5;
            coins_add[2]++;
            CoinCount.Content = Convert.ToString(actuallyCoin) + " руб";

            VndEntities database = new VndEntities();
            var coinscount = database.VendingMachineCoins.Where(a => a.CoinsId == 3).FirstOrDefault();
            coinscount.Count += 1;
            database.SaveChanges();
        }

        private void btn10(object sender, RoutedEventArgs e)
        {
            actuallyCoin += 10;
            coins_add[3]++;
            CoinCount.Content = Convert.ToString(actuallyCoin) + " руб";

            VndEntities database = new VndEntities();
            var coinscount = database.VendingMachineCoins.Where(a => a.CoinsId == 4).FirstOrDefault();
            coinscount.Count += 1;
            database.SaveChanges();
        }

        private void btn1(object sender, RoutedEventArgs e)
        {
            actuallyCoin += 1;
            coins_add[0]++;
            CoinCount.Content = Convert.ToString(actuallyCoin) + " руб";

            VndEntities database = new VndEntities();
            var coinscount = database.VendingMachineCoins.Where(a => a.CoinsId == 1).FirstOrDefault();
            coinscount.Count += 1;
            database.SaveChanges(); 

        }

        private void btn2(object sender, RoutedEventArgs e)
        {
            actuallyCoin += 2;
            coins_add[1]++;
            CoinCount.Content = Convert.ToString(actuallyCoin) + " руб";

            VndEntities database = new VndEntities();
            var coinscount = database.VendingMachineCoins.Where(a => a.CoinsId == 2).FirstOrDefault();
            coinscount.Count += 1;
            database.SaveChanges();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FrameAdmin.NavigationService.Navigate(new pages.Password());
        }

        private void FrameAdmin_Navigated(object sender, NavigationEventArgs e)
        {
        }

        private void FrmDrinks_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void lstDrink_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VndEntities database = new VndEntities();

            if (lstDrink.SelectedItem != null)
            {
                int drinkId = Convert.ToInt32((lstDrink.SelectedItem as Drinks).Id);
                var cntDrink = database.VendingMachineDrinks.Where(a => a.DrinksId == drinkId).FirstOrDefault();

                var profit = database.Report.Single(a => a.DrinkId == drinkId);
                if (actuallyCoin >= Convert.ToInt32((lstDrink.SelectedItem as Drinks).Cost))
                {
                    if (Convert.ToInt32(cntDrink.Count) != 0)
                    {
                        actuallyCoin -= Convert.ToInt32((lstDrink.SelectedItem as Drinks).Cost);
                        CoinCount.Content = Convert.ToString(actuallyCoin) + " руб.";

                        profit.Profit += Convert.ToInt32((lstDrink.SelectedItem as Drinks).Cost);
                        cntDrink.Count -= 1;
                        database.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Выбранного напитка нет в наличии");
                    }
                }
                else
                {
                    MessageBox.Show("Недостаточно денег на счету");
                }
            }
            lstDrink.SelectedItem = null;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {


        }

        private void TakeDrink_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CashOut_Click(object sender, RoutedEventArgs e)
        {
            VndEntities database = new VndEntities();
            int idmax = database.VendingMachineCoins.Max(id => id.CoinsId);
            int[] coins = new int[4];
            for (int i = 0; i < idmax; i++)
            {
                VendingMachineCoins nowCoin = database.VendingMachineCoins.Single(id => id.CoinsId == (i + 1));
                coins[i] = nowCoin.Count;
            }

            int CashOut = actuallyCoin;
            actuallyCoin = 0;
            CoinCount.Content = "0 руб.";
            int tens_out = CashOut / 10;
            if (coins[3] < tens_out)
            {
                CashOut -= coins[3] * 10;
                tens_out = coins[3];
            }
            else
            {
                CashOut -= tens_out * 10;
            }

            int fives_out = CashOut / 5;
            if (coins[2] < fives_out)
            {
                CashOut -= coins[2] * 5;
                fives_out = coins[2];
            }
            else
            {
                CashOut -= fives_out * 5;
            }


            int twos_out = CashOut / 2;
            if (coins[1] < twos_out)
            {
                CashOut -= coins[1] * 2;
                twos_out = coins[1];
            }
            else
            {
                CashOut -= twos_out * 2;
            }

            int ones_out = CashOut / 1;
            if (coins[0] < ones_out)
            {
                CashOut -= coins[0] * 1;
                ones_out = coins[0];
            }
            else
            {
                CashOut -= ones_out * 1;
            }

            coins[0] -= ones_out;
            coins[1] -= twos_out;
            coins[2] -= fives_out;
            coins[3] -= tens_out;

            for (int i = 0; i < idmax; i++)
            {
                VendingMachineCoins nowCoin = database.VendingMachineCoins.Single(id => id.CoinsId == (i + 1));
                nowCoin.Count = coins[i];

            }
            database.SaveChanges();
        }
    }
}
