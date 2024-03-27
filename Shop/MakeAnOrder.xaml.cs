using Shop.Shop_v2DataSetTableAdapters;
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

namespace Shop
{
    public partial class MakeAnOrder : Window
    {
        OrdersTableAdapter _ordersTableAdapter = new OrdersTableAdapter();
        Ordered_productsTableAdapter _ordered_ProductsTableAdapter = new Ordered_productsTableAdapter();
        int ID_User_mao;
        List<int> quantity = new List<int>();
        List<int> id = new List<int>();
        public MakeAnOrder(int ID_User, List<int> ID_Products, decimal TotalPrice, List<int> QuantityOfProduct)
        {
            InitializeComponent();
            quantity = QuantityOfProduct;
            id = ID_Products;
            TotalPriceLabel.Content = "Итого к оплате: " + TotalPrice.ToString() + " ₽";
            ID_User_mao = ID_User;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void nalichka_Checked(object sender, RoutedEventArgs e)
        {
            if (nalichka.IsChecked == true)
            {
                carta.IsChecked = false;
                BankCard.Visibility = Visibility.Collapsed;
            }
        }

        private void carta_Checked(object sender, RoutedEventArgs e)
        {
            if (carta.IsChecked == true)
            {
                nalichka.IsChecked = false;
                BankCard.Visibility= Visibility.Visible;
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < id.Count; i++)
            {
                for (int j = 0; j < quantity.Count; j++)
                {
                    _ordered_ProductsTableAdapter.InsertQueryOrderedProducts(ID_User_mao, id[i], quantity[j]);
                }
            }
            MessageBox.Show("Заказ был оформлен");
            this.Close();
        }
    }
}
