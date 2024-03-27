using MaterialDesignThemes.Wpf;
using Shop.Pages_in_the_users_window;
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
using System.Windows.Shapes;
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop.Administrator__manager__courirer__defaultuser_windows
{
    public partial class AdministratorWindow : Window
    {
        Product_categoriesTableAdapter product_categoriesTableAdapter = new Product_categoriesTableAdapter();
        string CategoryFilter;
        int ID_User_adm;
        int ID_Role_adm;
        public AdministratorWindow(int ID_User, int ID_Role)
        {
            InitializeComponent();
            ProductFilterCMBX.ItemsSource = product_categoriesTableAdapter.GetData();
            ProductFilterCMBX.DisplayMemberPath = "Category_name";
            if (ID_Role == 4)
            {
                SettingsButton.Visibility = Visibility.Collapsed;
            }
            this.WindowState = WindowState.Maximized;
            ID_Role_adm = ID_Role;
            ID_User_adm = ID_User;
            PageFrame.Content = new StorePage(ID_User);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Administrator__manager__courirer__client_windows.Administrator.Pages.PageChange(ID_Role_adm);
            StoreCataloge.Foreground = new SolidColorBrush(Colors.Black);
            FavoritesProductsButton.Foreground = new SolidColorBrush(Colors.Black);
            ShoppingBusket.Foreground = new SolidColorBrush(Colors.Black);
            AccountButton.Foreground = new SolidColorBrush(Colors.Black);
            SettingsButton.Foreground = new SolidColorBrush(Colors.Red);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new StorePage(ID_User_adm);
            StoreCataloge.Foreground = new SolidColorBrush(Colors.Red);
            FavoritesProductsButton.Foreground = new SolidColorBrush(Colors.Black);
            ShoppingBusket.Foreground = new SolidColorBrush(Colors.Black);
            AccountButton.Foreground = new SolidColorBrush(Colors.Black);
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        public void FavoritesProductsButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new FavoritesProducts(ID_User_adm);
            StoreCataloge.Foreground = new SolidColorBrush(Colors.Black);
            FavoritesProductsButton.Foreground = new SolidColorBrush(Colors.Red);
            ShoppingBusket.Foreground = new SolidColorBrush(Colors.Black);
            AccountButton.Foreground = new SolidColorBrush(Colors.Black);
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ShoppingBusket_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ShoppingBasket(ID_User_adm);
            StoreCataloge.Foreground = new SolidColorBrush(Colors.Black);
            FavoritesProductsButton.Foreground = new SolidColorBrush(Colors.Black);
            ShoppingBusket.Foreground = new SolidColorBrush(Colors.Red);
            AccountButton.Foreground = new SolidColorBrush(Colors.Black);
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new AccountInfo(ID_User_adm);
            StoreCataloge.Foreground = new SolidColorBrush(Colors.Black);
            FavoritesProductsButton.Foreground = new SolidColorBrush(Colors.Black);
            ShoppingBusket.Foreground = new SolidColorBrush(Colors.Black);
            AccountButton.Foreground = new SolidColorBrush(Colors.Red);
            SettingsButton.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
