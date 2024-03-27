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
using Shop.Administrator__manager__courirer__defaultuser_windows;
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop.Pages_in_the_users_window
{
    public partial class FavoritesProducts : Page
    {
        int ID_User_fp;
        int ID_Product;
        string IconKind;
        ProductsTableAdapter _productsTableAdapter = new ProductsTableAdapter();
        Users_favorites_productsTableAdapter _favorites_ProductsTableAdapter = new Users_favorites_productsTableAdapter();
        ShoppingBasketTableAdapter _shoppingBasket = new ShoppingBasketTableAdapter();
        private ObservableCollection<MyDataModel> myDataCollection = new ObservableCollection<MyDataModel>();
        private void SetPage()
        {
            myDataCollection.Clear();
            myGrid.ItemsSource = myDataCollection;
            var allProducts = _productsTableAdapter.GetData().Rows;
            var allUsers_favorites_products = _favorites_ProductsTableAdapter.GetData().Rows;
            var allShoppingBasket = _shoppingBasket.GetData().Rows;
            for (int j = 0; j < allUsers_favorites_products.Count; j++)
            {
                if (Convert.ToInt32(allUsers_favorites_products[j][0]) == ID_User_fp)
                {
                    for (int i = 0; i < allProducts.Count; i++)
                    {
                        int ID_Product = (int)allProducts[i][0];
                        if (Convert.ToInt32(allUsers_favorites_products[j][1]) == Convert.ToInt32(allProducts[i][0]))
                        {
                            IconKind = "CartPlus";
                            for (int k = 0; k < allShoppingBasket.Count; k++)
                            {
                                if (Convert.ToInt32(allShoppingBasket[k][0]) == ID_User_fp && Convert.ToInt32(allShoppingBasket[k][1]) == ID_Product)
                                {
                                    IconKind = "CartMinus";
                                }
                            }
                            myDataCollection.Add(new MyDataModel
                            {
                                Kind = IconKind,
                                ImageSource = allProducts[i][5].ToString(),
                                PriceProduct = allProducts[i][6].ToString() + " ₽",
                                DescriptionProduct = allProducts[i][1].ToString(),
                                CharacteristicsProduct = allProducts[i][3].ToString(),
                                InStockProduct = "В наличии: " + allProducts[i][8].ToString() + " шт."
                            });
                        }
                    }
                }
            }
        }
        public FavoritesProducts(int ID_User)
        {
            InitializeComponent();
            ID_User_fp = ID_User;
            SetPage();
        }
        public class MyDataModel
        {
            public string Kind { get; set; }
            public string ImageSource { get; set; }
            public string PriceProduct { get; set; }
            public string DescriptionProduct { get; set; }
            public string CharacteristicsProduct { get; set; }
            public string InStockProduct { get; set; }
        }

        private void AddToShoppingBasket_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("NameProductTextBlock") as TextBlock;
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
                _shoppingBasket.InsertQueryShoppingBusket(ID_User_fp, ID_Product, 1);
                productItem.Kind = "CartMinus";
            }
            else
            {
                _shoppingBasket.DeleteQueryShoppingBusket(ID_User_fp, ID_Product);
                productItem.Kind = "CartPlus";
            }
            myGrid.Items.Refresh();
        }

        private void DeleteFromFavorites_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("NameProductTextBlock") as TextBlock;
            string nameProduct = descriptionTextBlock.Text;
            MyDataModel productItem = button.DataContext as MyDataModel;
            var allProducts = _productsTableAdapter.GetData().Rows;
            for (int i = 0; i < allProducts.Count; i++)
            {
                if (allProducts[i][1].ToString() == nameProduct)
                {
                    ID_Product = Convert.ToInt32(allProducts[i][0]);
                    _favorites_ProductsTableAdapter.DeleteQueryUsers_favorites_products(ID_User_fp, ID_Product);
                    break;
                }
            }
            myGrid.Items.Refresh();
            SetPage();
        }

        private void ProductInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DependencyObject container = VisualTreeHelper.GetParent(button);
            TextBlock descriptionTextBlock = (container as Grid).FindName("NameProductTextBlock") as TextBlock;
            string nameProduct = descriptionTextBlock.Text;
            if (descriptionTextBlock != null)
            {
                string nameproduct = descriptionTextBlock.Text;
                GridPageFrame.Visibility = Visibility.Visible;
                MainCatalog.Visibility = Visibility.Collapsed;
                PageFrame.Content = new ProductInfo(nameproduct);
            }
        }
    }
}
