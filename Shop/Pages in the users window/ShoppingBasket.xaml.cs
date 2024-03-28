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
using Shop.Shop_v2DataSetTableAdapters;

namespace Shop.Pages_in_the_users_window
{
    public partial class ShoppingBasket : Page
    {
        decimal totalPrice = 0;
        int ID_User_sb;
        int ID_Product;
        string IconColor;
        ProductsTableAdapter _productsTableAdapter = new ProductsTableAdapter();
        ShoppingBasketTableAdapter _shoppingBasket = new ShoppingBasketTableAdapter();
        Users_favorites_productsTableAdapter _favorites_ProductsTableAdapter = new Users_favorites_productsTableAdapter();
        private ObservableCollection<MyDataModel> myDataCollection = new ObservableCollection<MyDataModel>();
        public void SetPage()
        {
            myDataCollection.Clear();
            myGrid.ItemsSource = myDataCollection;
            var allProducts = _productsTableAdapter.GetData().Rows;
            var allShoppingBasket = _shoppingBasket.GetData().Rows;
            var allUsers_favorites_products = _favorites_ProductsTableAdapter.GetData().Rows;
            for (int i = 0; i < allShoppingBasket.Count; i++)
            {
                if (Convert.ToInt32(allShoppingBasket[i][0]) == ID_User_sb)
                {
                    for (int j = 0; j < allProducts.Count; j++)
                    {
                        if (Convert.ToInt32(allShoppingBasket[i][1]) == Convert.ToInt32(allProducts[j][0]))
                        {
                            int ID_Product = (int)allProducts[j][0];
                            IconColor = "DarkGray";
                            for (int k = 0; k < allUsers_favorites_products.Count; k++)
                            {
                                if (Convert.ToInt32(allUsers_favorites_products[k][0]) == ID_User_sb && Convert.ToInt32(allUsers_favorites_products[k][1]) == Convert.ToInt32(allShoppingBasket[i][1]))
                                {
                                    IconColor = "Red";
                                }
                            }
                            myDataCollection.Add(new MyDataModel
                            {
                                Kind = IconColor,
                                ImageSource = allProducts[j][5].ToString(),
                                PriceProduct = (Convert.ToInt32(allProducts[j][6])* Convert.ToInt32(allShoppingBasket[i][2])).ToString() + " ₽",
                                NameProduct = allProducts[j][1].ToString(),
                                CharacteristicsProduct = allProducts[j][3].ToString(),
                                QuantityOfProduct = allShoppingBasket[i][2].ToString(),
                            });
                        }
                    }
                }
            }
            totalPrice = 0;
            foreach (var dataModel in myDataCollection)
            {
                string price = dataModel.PriceProduct;
                if (decimal.TryParse(price.Replace(" ₽", ""), out decimal priceValue))
                {
                    totalPrice += priceValue;
                }
            }
            TotalPrice.Content = "Итого к оплате: " + totalPrice.ToString() + " ₽";
        }
        public ShoppingBasket(int ID_User)
        {
            InitializeComponent();
            ID_User_sb = ID_User;
            SetPage();
        }
        public class MyDataModel
        {
            public string Kind { get; set; }
            public string ImageSource { get; set; }
            public string PriceProduct { get; set; }
            public string NameProduct { get; set; }
            public string CharacteristicsProduct { get; set; }
            public string QuantityOfProduct { get; set; }
        }

        private void DeleteProductFromSB_Click(object sender, RoutedEventArgs e)
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
                    _shoppingBasket.DeleteQueryShoppingBusket(ID_User_sb, ID_Product);
                    break;
                }
            }
            myGrid.Items.Refresh();
            SetPage();
        }

        private void AddToFavoritesProducts_Click(object sender, RoutedEventArgs e)
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
                }
            }
            if (productItem.Kind == "Red")
            {
                _favorites_ProductsTableAdapter.DeleteQueryUsers_favorites_products(ID_User_sb, ID_Product);
                productItem.Kind = "DarkGray";
            }
            else
            {
                _favorites_ProductsTableAdapter.InsertQueryUsersFavoritesProducts(ID_User_sb, ID_Product);
                productItem.Kind = "Red";
            }
            myGrid.Items.Refresh();
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
                ShoppingBasketSV.Visibility = Visibility.Collapsed;
                PageFrame.Content = new ProductInfo(nameproduct);
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
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
                    int ID_Product = Convert.ToInt32(allProducts[i][0]);
                    var allShoppingBasket = _shoppingBasket.GetData().Rows;
                    for (int j = 0; j < allShoppingBasket.Count; j++)
                    {
                        if (Convert.ToInt32(allShoppingBasket[j][0]) == ID_User_sb && Convert.ToInt32(allShoppingBasket[j][1]) == ID_Product)
                        {
                            if (Convert.ToInt32(allShoppingBasket[j][2]) < Convert.ToInt32(allProducts[i][8]))
                            {
                                int NewQuantityValue = Convert.ToInt32(allShoppingBasket[j][2]) + 1;
                                _shoppingBasket.UpdateQuntityInShoppingBasket(NewQuantityValue, ID_Product, ID_User_sb);
                                SetPage();
                            }
                        }
                    }
                }
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
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
                    int ID_Product = Convert.ToInt32(allProducts[i][0]);
                    var allShoppingBasket = _shoppingBasket.GetData().Rows;
                    for (int j = 0; j < allShoppingBasket.Count; j++)
                    {
                        if (Convert.ToInt32(allShoppingBasket[j][0]) == ID_User_sb && Convert.ToInt32(allShoppingBasket[j][1]) == ID_Product)
                        {
                            if (Convert.ToInt32(allShoppingBasket[j][2]) > 1)
                            {
                                int NewQuantityValue = Convert.ToInt32(allShoppingBasket[j][2]) - 1;
                                _shoppingBasket.UpdateQuntityInShoppingBasket(NewQuantityValue, ID_Product, ID_User_sb);
                                SetPage();
                            }
                        }
                    }
                }
            }
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (totalPrice > 0)
            {
                MakeAnOrder makeAnOrder = new MakeAnOrder(ID_User_sb, totalPrice);
                makeAnOrder.Show();
            }
            else if (totalPrice <= 0)
            {
                MessageBox.Show("Корзина пуста");
            }
        }
    }
}
