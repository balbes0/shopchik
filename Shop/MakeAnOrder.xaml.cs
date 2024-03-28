using Shop.Pages_in_the_users_window;
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
        ShoppingBasketTableAdapter _shoppingBasketTableAdapter = new ShoppingBasketTableAdapter();
        Order_receiptsTableAdapter _order_ReceiptsTableAdapter = new Order_receiptsTableAdapter();
        int ID_User_mao;
        decimal totalprice;
        int ID_Payment_method;
        int valuelastrow;
        public MakeAnOrder(int ID_User, decimal TotalPrice)
        {
            InitializeComponent();
            TotalPriceLabel.Content = "Итого к оплате: " + TotalPrice.ToString() + " ₽";
            ID_User_mao = ID_User;
            totalprice = TotalPrice;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void nalichka_Checked(object sender, RoutedEventArgs e)
        {
            if (nalichka.IsChecked == true)
            {
                ID_Payment_method = 2;
                carta.IsChecked = false;
                BankCard.Visibility = Visibility.Collapsed;
            }
        }

        private void carta_Checked(object sender, RoutedEventArgs e)
        {
            if (carta.IsChecked == true)
            {
                ID_Payment_method = 1;
                nalichka.IsChecked = false;
                BankCard.Visibility= Visibility.Visible;
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            var allshopingbaskets = _shoppingBasketTableAdapter.GetData().Rows;
            var allorders = _ordersTableAdapter.GetData().Rows;
            for (int k = 0; k < allorders.Count; k++)
            {
                if (k == allorders.Count - 1)
                {
                    valuelastrow = Convert.ToInt32(allorders[k][0]);
                }
            }
            for (int i = 0; i < allshopingbaskets.Count; i++)
            {
                if (Convert.ToInt32(allshopingbaskets[i][0]) == ID_User_mao)
                {
                    int ID_Product = Convert.ToInt32(allshopingbaskets[i][1]);
                    int Quantity = Convert.ToInt32(allshopingbaskets[i][2]);
                    _ordered_ProductsTableAdapter.InsertQueryOrderedProducts(ID_User_mao, ID_Product, Quantity);
                }
            }
            for (int j = 0; j < allshopingbaskets.Count; j++)
            {
                if (Convert.ToInt32(allshopingbaskets[j][0]) == ID_User_mao)
                {
                    int ID_Product = Convert.ToInt32(allshopingbaskets[j][1]);
                    _shoppingBasketTableAdapter.DeleteQueryShoppingBusket(ID_User_mao, ID_Product);
                }
            }
            string currentDateTime = DateTime.Now.ToString();
            currentDateTime += " был совершен заказ;";
            _ordersTableAdapter.InsertQueryOrders(ID_User_mao, totalprice, ID_Payment_method, currentDateTime, AddressDelivery.Text, 1);
            for (int k = 0; k < allorders.Count; k++)
            {
                if (k == allorders.Count - 1)
                {
                    valuelastrow = Convert.ToInt32(allorders[k][0]);
                }
            }
            _order_ReceiptsTableAdapter.InsertQueryOrder_receipts(valuelastrow);
            MessageBox.Show("Заказ был оформлен");
            this.Close();
        }
    }
}
