using System;
using System.Collections.Generic;
using System.Data;
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
using Shop.Shop_v2DataSetTableAdapters;
namespace Shop.Pages_in_the_users_window
{
    public partial class Orders : Page
    {
        DeliveredOrderViewTableAdapter _deliveredOrderViewTableAdapter = new DeliveredOrderViewTableAdapter();
        DeliveredOrderTableAdapter _deliveredOrderTableAdapter = new DeliveredOrderTableAdapter();

        ReceiptsViewTableAdapter _receiptsViewTableAdapter = new ReceiptsViewTableAdapter();
        Order_receiptsTableAdapter _order_ReceiptsTableAdapter = new Order_receiptsTableAdapter();

        OrderedProductsViewTableAdapter _orderedProductsViewTableAdapter = new OrderedProductsViewTableAdapter();
        Ordered_productsTableAdapter _ordered_ProductsTableAdapter = new Ordered_productsTableAdapter();

        OrderViewxTableAdapter _orderViewxTableAdapter = new OrderViewxTableAdapter();
        OrdersTableAdapter _ordersTableAdapter = new OrdersTableAdapter();

        Payment_methodsTableAdapter _payment_MethodsTableAdapter = new Payment_methodsTableAdapter();
        ProductsTableAdapter _productsTableAdapter = new ProductsTableAdapter();

        Order_statusesTableAdapter _order_StatusesTableAdapter = new Order_statusesTableAdapter();
        string selectedTable;
        int ID_User_o;
        int Payment_method_ID;
        int Order_status_ID;
        int ID_Productx;
        public Orders(int ID_User)
        {
            InitializeComponent();
            ID_User_o = ID_User;
            PaymentMethodCMBX.ItemsSource = _payment_MethodsTableAdapter.GetData();
            PaymentMethodCMBX.DisplayMemberPath = "Payment_method_name";
            TovarOPCMBX.ItemsSource = _productsTableAdapter.GetData();
            TovarOPCMBX.DisplayMemberPath = "Product_name";
            OrderStatusTBX.ItemsSource = _order_StatusesTableAdapter.GetData();
            OrderStatusTBX.DisplayMemberPath = "Status_name";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ordersp.IsSelected)
            {
                GridForOrderedProducts.Visibility = Visibility.Collapsed;
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Visible;
                GridWithButtons.Visibility = Visibility.Visible;
                GridWithButtonForChangeStatus.Visibility = Visibility.Collapsed;
                selectedTable = "Orders";
                DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
            }
            else if (DeliveredOrdersp.IsSelected)
            {
                GridForOrderedProducts.Visibility = Visibility.Collapsed;
                InsertButton.Visibility = Visibility.Visible;
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Visible;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtons.Visibility = Visibility.Visible;
                GridWithButtonForChangeStatus.Visibility = Visibility.Collapsed;
                selectedTable = "DeliveredOrders";
                DataGridInOrders.ItemsSource = _deliveredOrderViewTableAdapter.GetData();
            }
            else if (OrderedProductsp.IsSelected)
            {
                GridForOrderedProducts.Visibility = Visibility.Visible;
                InsertButton.Visibility = Visibility.Visible;
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtons.Visibility = Visibility.Visible;
                selectedTable = "OrderedProducts";
                DataGridInOrders.ItemsSource = _orderedProductsViewTableAdapter.GetData();
            }
            else if (OrderReceiptsp.IsSelected)
            {
                GridForOrderedProducts.Visibility = Visibility.Collapsed;
                InsertButton.Visibility = Visibility.Visible;
                GridForOrderReceipts.Visibility = Visibility.Visible;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtons.Visibility = Visibility.Visible;
                GridWithButtonForChangeStatus.Visibility = Visibility.Collapsed;
                selectedTable = "OrderReceipts";
                DataGridInOrders.ItemsSource = _receiptsViewTableAdapter.GetData();
            }
            else if (ChangeOrderStatus.IsSelected)
            {
                GridForOrderedProducts.Visibility = Visibility.Collapsed;
                selectedTable = "ChangeOrderStatus";
                DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtonForChangeStatus.Visibility = Visibility.Visible;
                GridWithButtons.Visibility = Visibility.Collapsed;
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("я запрещаю вам вводить данные в эти таблицы");
        }

        private void TotalPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridInOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridInOrders.SelectedItem != null)
            {
                switch (selectedTable)
                {
                    case "Orders":
                        TotalPrice.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[3].ToString();
                        PaymentDetailsTBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[8].ToString();
                        PaymentMethodCMBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[5].ToString();
                        DeliveryAddressTBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[6].ToString();
                        OrderStatusTBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[7].ToString();
                        UserIDTBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                    case "DeliveredOrders":
                        IDUserDO.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[1].ToString();
                        OrderIDDO.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[3].ToString();
                        break;
                    case "OrderedProducts":
                        TovarOPCMBX.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[5].ToString();
                        IDUserOP.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[1].ToString();
                        QuantityOP.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[4].ToString();
                        break;
                    case "OrderReceipts":
                        IDOrderRO.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                    case "ChangeOrderStatus":
                        IDOrderRO.Text = (DataGridInOrders.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                }
            }
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOrders.SelectedItem != null)
            {
                object id = (DataGridInOrders.SelectedItem as DataRowView).Row[0];
                _ordersTableAdapter.UpdateQueryOrderStatus(2, Convert.ToInt32(id), ID_User_o);
                DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
            }
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOrders.SelectedItem != null)
            {
                object id = (DataGridInOrders.SelectedItem as DataRowView).Row[0];
                _ordersTableAdapter.UpdateQueryOrderStatus(5, Convert.ToInt32(id), ID_User_o);
                DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOrders.SelectedItem != null)
            {
                object id = (DataGridInOrders.SelectedItem as DataRowView).Row[0];
                switch (selectedTable)
                {
                    case "Orders":
                        _ordersTableAdapter.DeleteQueryOrders(Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
                        break;
                    case "DeliveredOrders":
                        _deliveredOrderTableAdapter.DeleteQueryDeliveredOrder(Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _deliveredOrderViewTableAdapter.GetData();
                        break;
                    case "OrderedProducts":
                        _ordered_ProductsTableAdapter.DeleteQueryOrderedProducts(Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _orderedProductsViewTableAdapter.GetData();
                        break;
                    case "OrderReceipts":
                        _order_ReceiptsTableAdapter.DeleteQueryOrder_receipts(Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _receiptsViewTableAdapter.GetData();
                        break;
                }
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOrders.SelectedItem != null)
            {
                object id = (DataGridInOrders.SelectedItem as DataRowView).Row[0];
                switch (selectedTable)
                {
                    case "Orders":
                        _ordersTableAdapter.UpdateQueryOrders(Convert.ToInt32(UserIDTBX.Text), Convert.ToDecimal(TotalPrice.Text), Payment_method_ID, PaymentDetailsTBX.Text, DeliveryAddressTBX.Text, Order_status_ID, Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
                        break;
                    case "DeliveredOrders":
                        _deliveredOrderTableAdapter.UpdateQueryDelivered_Order(Convert.ToInt32(IDUserDO.Text), Convert.ToInt32(OrderIDDO.Text), Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _deliveredOrderViewTableAdapter.GetData();
                        break;
                    case "OrderedProducts":
                        _ordered_ProductsTableAdapter.UpdateQueryOrdered_products(Convert.ToInt32(IDUserOP.Text), ID_Productx, Convert.ToInt32(QuantityOP.Text), Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _orderedProductsViewTableAdapter.GetData();
                        break;
                    case "OrderReceipts":
                        _order_ReceiptsTableAdapter.UpdateQueryOrder_receipts(Convert.ToInt32(IDOrderRO.Text), Convert.ToInt32(id));
                        DataGridInOrders.ItemsSource = _receiptsViewTableAdapter.GetData();
                        break;
                }
            }
        }

        private void PaymentMethodCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)PaymentMethodCMBX.SelectedItem;
            Payment_method_ID = (int)selectedRow["ID_Payment_method"];
        }

        private void OrderStatusTBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)OrderStatusTBX.SelectedItem;
            Order_status_ID = (int)selectedRow["ID_Order_status"];
        }

        private void TovarOPCMBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)TovarOPCMBX.SelectedItem;
            ID_Productx = (int)selectedRow["ID_Product"];
        }
    }
}
