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
    public partial class ChangeOtherTables : Page
    {
        BrandsTableAdapter _brandstableAdapter = new BrandsTableAdapter();
        Order_statusesViewTableAdapter _order_StatusesViewTableAdapter = new Order_statusesViewTableAdapter();
        Order_statusesTableAdapter _order_StatusesTableAdapter = new Order_statusesTableAdapter();
        Product_categoriesTableAdapter _product_CategoriesTableAdapter = new Product_categoriesTableAdapter();
        Users_rolesTableAdapter _users_RolesTableAdapter = new Users_rolesTableAdapter();
        Payment_methodsTableAdapter _payment_MethodsTableAdapter = new Payment_methodsTableAdapter();
        BrandsViewTableAdapter _brandsViewTableAdapter = new BrandsViewTableAdapter();
        ProductCategoriesViewTableAdapter _productCategoriesViewTable = new ProductCategoriesViewTableAdapter();
        UsersRolesViewTableAdapter _usersRolesViewTable = new UsersRolesViewTableAdapter();
        PaymentMethodsView _paymentMethodsView = new PaymentMethodsView();
        string selectedTable;
        public ChangeOtherTables()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChangeBrands.IsSelected)
            {
                selectedTable = "Brands";
                DataGridInOtherPage.ItemsSource = _brandsViewTableAdapter.GetData();
                GridForOrderStatuses.Visibility = Visibility.Collapsed;
                GridForBrands.Visibility = Visibility.Visible;
                GridForPaymentMethods.Visibility = Visibility.Collapsed;
                GridForProductsCategories.Visibility = Visibility.Collapsed;
                GridForUsersRoles.Visibility = Visibility.Collapsed;
            }
            if (ChangeCategorieProducts.IsSelected)
            {
                selectedTable = "ProductCategories";
                DataGridInOtherPage.ItemsSource = _productCategoriesViewTable.GetData();
                GridForOrderStatuses.Visibility = Visibility.Collapsed;
                GridForBrands.Visibility = Visibility.Collapsed;
                GridForPaymentMethods.Visibility = Visibility.Collapsed;
                GridForProductsCategories.Visibility = Visibility.Visible;
                GridForUsersRoles.Visibility = Visibility.Collapsed;
            }
            if (ChangeUsersRoles.IsSelected)
            {
                selectedTable = "UsersRoles";
                DataGridInOtherPage.ItemsSource = _usersRolesViewTable.GetData();
                GridForOrderStatuses.Visibility = Visibility.Collapsed;
                GridForBrands.Visibility = Visibility.Collapsed;
                GridForPaymentMethods.Visibility = Visibility.Collapsed;
                GridForProductsCategories.Visibility = Visibility.Collapsed;
                GridForUsersRoles.Visibility = Visibility.Visible;
            }
            if (ChangePaymentMethods.IsSelected)
            {
                selectedTable = "PaymentMethods";
                DataGridInOtherPage.ItemsSource = _paymentMethodsView.GetData();
                GridForOrderStatuses.Visibility = Visibility.Collapsed;
                GridForBrands.Visibility = Visibility.Collapsed;
                GridForPaymentMethods.Visibility = Visibility.Visible;
                GridForProductsCategories.Visibility = Visibility.Collapsed;
                GridForUsersRoles.Visibility = Visibility.Collapsed;
            }
            if (ChangeOrderStatus.IsSelected)
            {
                selectedTable = "OrderStatuses";
                DataGridInOtherPage.ItemsSource = _order_StatusesViewTableAdapter.GetData();
                GridForOrderStatuses.Visibility = Visibility.Visible;
                GridForBrands.Visibility = Visibility.Collapsed;
                GridForPaymentMethods.Visibility = Visibility.Collapsed;
                GridForProductsCategories.Visibility = Visibility.Collapsed;
                GridForUsersRoles.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOtherPage.SelectedItem != null)
            {
                object id = (DataGridInOtherPage.SelectedItem as DataRowView).Row[0];
                switch (selectedTable)
                {
                    case "Brands":
                        _brandstableAdapter.DeleteQueryBrands(Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _brandsViewTableAdapter.GetData();
                        break;
                    case "ProductCategories":
                        _product_CategoriesTableAdapter.DeleteQueryProductCategories(Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _productCategoriesViewTable.GetData();
                        break;
                    case "UsersRoles":
                        _users_RolesTableAdapter.DeleteQueryUsersRoles(Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _usersRolesViewTable.GetData();
                        break;
                    case "PaymentMethods":
                        _payment_MethodsTableAdapter.DeleteQueryPaymentMethods(Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _paymentMethodsView.GetData();
                        break;
                    case "OrderStatuses":
                        _order_StatusesTableAdapter.DeleteQueryOrder_statuses(Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _order_StatusesViewTableAdapter.GetData();
                        break;
                }
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            switch (selectedTable)
            {
                case "Brands":
                    _brandstableAdapter.InsertQueryBrands(NameBrandsTBX.Text, NameCountryTBX.Text);
                    DataGridInOtherPage.ItemsSource = _brandsViewTableAdapter.GetData();
                    break;
                case "ProductCategories":
                    _product_CategoriesTableAdapter.InsertQueryProductCategories(CategoryNameTBX.Text);
                    DataGridInOtherPage.ItemsSource = _productCategoriesViewTable.GetData();
                    break;
                case "UsersRoles":
                    _users_RolesTableAdapter.InsertQueryUsersRoles(NameRoleTBX.Text);
                    DataGridInOtherPage.ItemsSource = _usersRolesViewTable.GetData();
                    break;
                case "PaymentMethods":
                    _payment_MethodsTableAdapter.InsertQueryPaymentMethods(PaymentMethod.Text);
                    DataGridInOtherPage.ItemsSource = _paymentMethodsView.GetData();
                    break;
                case "OrderStatuses":
                    _order_StatusesTableAdapter.InsertQueryOrder_statuses(OrderStatusTBX.Text);
                    DataGridInOtherPage.ItemsSource = _order_StatusesViewTableAdapter.GetData();
                    break;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            NameBrandsTBX.Text = "";
            NameCountryTBX.Text = "";
            CategoryNameTBX.Text = "";
            NameRoleTBX.Text = "";
            PaymentMethod.Text = "";
            OrderStatusTBX.Text = "";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInOtherPage.SelectedItem != null)
            {
                object id = (DataGridInOtherPage.SelectedItem as DataRowView).Row[0];
                switch (selectedTable)
                {
                    case "Brands":
                        _brandstableAdapter.UpdateQueryBrands(NameBrandsTBX.Text, NameCountryTBX.Text, Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _brandsViewTableAdapter.GetData();
                        break;
                    case "ProductCategories":
                        _product_CategoriesTableAdapter.UpdateQueryProductsCategories(CategoryNameTBX.Text, Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _productCategoriesViewTable.GetData();
                        break;
                    case "UsersRoles":
                        _users_RolesTableAdapter.UpdateQueryUsersRoles(NameRoleTBX.Text, Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _usersRolesViewTable.GetData();
                        break;
                    case "PaymentMethods":
                        _payment_MethodsTableAdapter.UpdateQueryPaymentMethods(PaymentMethod.Text, Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _paymentMethodsView.GetData();
                        break;
                    case "OrderStatuses":
                        _order_StatusesTableAdapter.UpdateQueryOrder_statuses(OrderStatusTBX.Text, Convert.ToInt32(id));
                        DataGridInOtherPage.ItemsSource = _order_StatusesViewTableAdapter.GetData();
                        break;
                }
            }
        }

        private void DataGridInOtherPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridInOtherPage.SelectedItem != null)
            {
                switch (selectedTable)
                {
                    case "Brands":
                        NameBrandsTBX.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[1].ToString();
                        NameCountryTBX.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[2].ToString();
                        break;
                    case "ProductCategories":
                        CategoryNameTBX.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                    case "UsersRoles":
                        NameRoleTBX.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                    case "PaymentMethods":
                        PaymentMethod.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                    case "OrderStatuses":
                        OrderStatusTBX.Text = (DataGridInOtherPage.SelectedItem as DataRowView).Row[1].ToString();
                        break;
                }
            }
        }
    }
}
