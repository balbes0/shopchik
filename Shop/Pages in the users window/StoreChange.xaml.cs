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
    public partial class StoreChange : Page
    {
        ProductsTableAdapter _productsTableAdapter = new ProductsTableAdapter();
        ProductsViewTableAdapter _productsViewTableAdapter = new ProductsViewTableAdapter();
        BrandsTableAdapter _brandsTableAdapter = new BrandsTableAdapter();
        Product_categoriesTableAdapter _product_CategoriesTableAdapter = new Product_categoriesTableAdapter();
        UsersTableAdapter _usersTableAdapter = new UsersTableAdapter();
        public StoreChange()
        {
            InitializeComponent();
            DataGridInStoreChangePage.ItemsSource = _productsViewTableAdapter.GetData();
            Brand_IDCMBX.ItemsSource = _brandsTableAdapter.GetData();
            Brand_IDCMBX.DisplayMemberPath = "Brand_name";
            Category_IDCMBX.ItemsSource = _product_CategoriesTableAdapter.GetData();
            Category_IDCMBX.DisplayMemberPath = "Category_name";
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            var allproduct = _productsTableAdapter.GetData().Rows;
            for (int i = 0; i < allproduct.Count; i++)
            {
                if (allproduct[i][1].ToString() == Product_nameTBX.Text)
                {
                    flag = false;
                    MessageBox.Show("Товар с таким же именем уже существует");
                    break;
                }
            }
            if (flag)
            {
                DataRowView selectedRow = (DataRowView)Brand_IDCMBX.SelectedItem;
                int Brand_ID = (int)selectedRow["ID_Brand"];
                DataRowView selectedRow2 = (DataRowView)Category_IDCMBX.SelectedItem;
                int Category_ID = (int)selectedRow2["ID_Category"];
                _productsTableAdapter.InsertQueryProducts(Product_nameTBX.Text, Product_descriptionTBX.Text, Product_characteristics.Text, Brand_ID, Path_to_product_imageTBX.Text, Convert.ToDecimal(Product_priceTBX.Text), Category_ID, Convert.ToInt32(Products_in_stockTBX.Text));
                DataGridInStoreChangePage.ItemsSource = _productsViewTableAdapter.GetData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            object id = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[0];
            _productsTableAdapter.DeleteQueryProducts(Convert.ToInt32(id));
            DataGridInStoreChangePage.ItemsSource = _productsViewTableAdapter.GetData();
        }

        private void DataGridInStoreChangePage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridInStoreChangePage.SelectedItem != null)
            {
                Product_nameTBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[1].ToString();
                Product_descriptionTBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[2].ToString();
                Product_characteristics.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[3].ToString();
                Product_characteristics.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[3].ToString();
                Brand_IDCMBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[4].ToString();
                Product_priceTBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[5].ToString();
                Category_IDCMBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[6].ToString();
                Products_in_stockTBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[7].ToString();
                Path_to_product_imageTBX.Text = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[8].ToString();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Product_nameTBX.Text = "";
            Product_descriptionTBX.Text = "";
            Product_characteristics.Text = "";
            Product_characteristics.Text = "";
            Brand_IDCMBX.Text = null;
            Path_to_product_imageTBX.Text = "";
            Product_priceTBX.Text = "";
            Category_IDCMBX.Text = null;
            Products_in_stockTBX.Text = "";
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridInStoreChangePage.SelectedItem != null)
            {
                object ID_Product = (DataGridInStoreChangePage.SelectedItem as DataRowView).Row[0];
                DataRowView selectedRow = (DataRowView)Brand_IDCMBX.SelectedItem;
                int Brand_ID = (int)selectedRow["ID_Brand"];
                DataRowView selectedRow2 = (DataRowView)Category_IDCMBX.SelectedItem;
                int Category_ID = (int)selectedRow2["ID_Category"];
                _productsTableAdapter.UpdateProductByID(Product_nameTBX.Text, Product_descriptionTBX.Text, Product_characteristics.Text, Brand_ID, Path_to_product_imageTBX.Text, Convert.ToDecimal(Product_priceTBX.Text), Category_ID, Convert.ToInt32(Products_in_stockTBX.Text), Convert.ToInt32(ID_Product));
                DataGridInStoreChangePage.ItemsSource = _productsViewTableAdapter.GetData();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<productmodel> forimport = jsonconverter.DeserializeObject<List<productmodel>>();
            foreach (var item in forimport)
            {
                _productsTableAdapter.InsertQueryProducts(item.Product_name, item.Product_description, item.Product_characteristics, item.Brand_ID, item.Path_to_image, item.Product_price, item.Category_ID, item.Product_in_stock);
            }
            DataGridInStoreChangePage.ItemsSource = _productsViewTableAdapter.GetData();
        }
    }
}
