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
        string selectedTable;
        public Orders()
        {
            InitializeComponent();
            PaymentMethodCMBX.ItemsSource = _payment_MethodsTableAdapter.GetData();
            PaymentMethodCMBX.DisplayMemberPath = "Payment_method_name";
            ProductsCMBX.ItemsSource = _productsTableAdapter.GetData();
            ProductsCMBX.DisplayMemberPath = "Product_name";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ordersp.IsSelected)
            {
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
                InsertButton.Visibility = Visibility.Visible;
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtons.Visibility = Visibility.Visible;
                GridWithButtonForChangeStatus.Visibility = Visibility.Collapsed;
                selectedTable = "OrderedProducts";
                DataGridInOrders.ItemsSource = _orderedProductsViewTableAdapter.GetData();
            }
            else if (OrderReceiptsp.IsSelected)
            {
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
                selectedTable = "ChangeOrderStatus";
                DataGridInOrders.ItemsSource = _orderViewxTableAdapter.GetData();
                GridForOrderReceipts.Visibility = Visibility.Collapsed;
                GridForDeliveredOrders.Visibility = Visibility.Collapsed;
                GridForOrders.Visibility = Visibility.Collapsed;
                GridWithButtonForChangeStatus.Visibility = Visibility.Visible;
                GridWithButtons.Visibility = Visibility.Collapsed;
            }
        }

    }
}
