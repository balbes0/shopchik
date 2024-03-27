using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MaterialDesignThemes.Wpf;
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop.Pages_in_the_users_window
{
    public partial class StorePage : Page
    {
        int ID_User_storepg;
        string foregroundcolor;
        string IconKind;
        int ID_Product;
        ProductsTableAdapter _productsTableAdapter = new ProductsTableAdapter();
        Users_favorites_productsTableAdapter _favorites_ProductsTableAdapter = new Users_favorites_productsTableAdapter();
        ShoppingBasketTableAdapter _shoppingBasket = new ShoppingBasketTableAdapter();
        private ObservableCollection<MyDataModel> myDataCollection = new ObservableCollection<MyDataModel>();
        private ObservableCollection<MyDataModel> myFilteredDataCollection = new ObservableCollection<MyDataModel>();
        public StorePage(int ID_User)
        {
            InitializeComponent();
            ID_User_storepg = ID_User;
            myGrid.ItemsSource = myDataCollection;
            var allProducts = _productsTableAdapter.GetData().Rows;
            var allUsers_favorites_products = _favorites_ProductsTableAdapter.GetData().Rows;
            var allShoppingBasket = _shoppingBasket.GetData().Rows;
            for (int i = 0; i < allProducts.Count; i++)
            {
                int ID_Product = (int)allProducts[i][0];
                foregroundcolor = "DarkGray";
                IconKind = "CartPlus";
                for (int j = 0; j < allUsers_favorites_products.Count; j++)
                {
                    if (Convert.ToInt32(allUsers_favorites_products[j][0]) == ID_User_storepg && Convert.ToInt32(allUsers_favorites_products[j][1]) == ID_Product)
                    {
                        foregroundcolor = "Red";
                    }
                }
                for (int k = 0; k < allShoppingBasket.Count; k++)
                {
                    if (Convert.ToInt32(allShoppingBasket[k][0]) == ID_User_storepg && Convert.ToInt32(allShoppingBasket[k][1]) == ID_Product)
                    {
                        IconKind = "CartMinus";
                    }
                }
                myDataCollection.Add(new MyDataModel
                {
                    ForegroundColor = foregroundcolor,
                    Kind = IconKind,
                    ImageSource = allProducts[i][5].ToString(),
                    Price = allProducts[i][6].ToString() + " ₽",
                    Description = allProducts[i][1].ToString(),
                    Details = allProducts[i][3].ToString()
                });
            }
        }
        public class MyDataModel
        {
            public string ForegroundColor { get; set; }
            public string ImageSource { get; set; }
            public string Price { get; set; }
            public string Description { get; set; }
            public string Details { get; set; }
            public string Kind { get; set; }
        }

        private void PageProductInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("DescriptionTextBlock") as TextBlock;
            if (descriptionTextBlock != null)
            {
                string nameproduct = descriptionTextBlock.Text;
                PageFrameGrid.Visibility = Visibility.Visible;
                MainCatalog.Visibility = Visibility.Collapsed;
                PageFrame.Content = new ProductInfo(nameproduct);
            }
        }

        private void AddToFavoritesButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("DescriptionTextBlock") as TextBlock;
            string nameProduct = descriptionTextBlock.Text;
            MyDataModel productItem = button.DataContext as MyDataModel;
            var allProducts = _productsTableAdapter.GetData().Rows;
            for (int i = 0; i < allProducts.Count; i++)
            {
                if (allProducts[i][1].ToString() == nameProduct)
                {
                    ID_Product = Convert.ToInt32(allProducts[i][0]);
                }
            }
            if (productItem.ForegroundColor == "Red")
            {
                _favorites_ProductsTableAdapter.DeleteQueryUsers_favorites_products(ID_User_storepg, ID_Product);
                productItem.ForegroundColor = "DarkGray";
            }
            else
            {
                _favorites_ProductsTableAdapter.InsertQueryUsersFavoritesProducts(ID_User_storepg, ID_Product);
                productItem.ForegroundColor = "Red";
            }
            myGrid.Items.Refresh();
        }

        private void AddToShoppingBasket_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("DescriptionTextBlock") as TextBlock;
            string nameProduct = descriptionTextBlock.Text;
            var allProducts = _productsTableAdapter.GetData().Rows;
            for (int i = 0; i < allProducts.Count; i++)
            {
                if (allProducts[i][1].ToString() == nameProduct)
                {
                    ID_Product = Convert.ToInt32(allProducts[i][0]);
                }
            }
            MyDataModel productItem = button.DataContext as MyDataModel;
            if (productItem.Kind == "CartPlus")
            {
                _shoppingBasket.InsertQueryShoppingBusket(ID_User_storepg, ID_Product, 1);
                productItem.Kind = "CartMinus";
            }
            else
            {
                _shoppingBasket.DeleteQueryShoppingBusket(ID_User_storepg, ID_Product);
                productItem.Kind = "CartPlus";
            }
            myGrid.Items.Refresh();
        }
    }
}
