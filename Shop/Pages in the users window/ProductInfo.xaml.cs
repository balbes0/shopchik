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
using static Shop.Pages_in_the_users_window.StorePage;

namespace Shop.Pages_in_the_users_window
{
    public partial class ProductInfo : Page
    {
        ProductsViewTableAdapter _productsTableAdapter = new ProductsViewTableAdapter();
        public ProductInfo(string nameproduct)
        {
            InitializeComponent();
            var allProducts = _productsTableAdapter.GetData().Rows;
            for (int i = 0; i < allProducts.Count; i++)
            {
                int ID_Product = (int)allProducts[i][0];
                if (allProducts[i][1].ToString() == nameproduct)
                {
                    InStockTextBlock.Text = allProducts[i][7].ToString() + " шт.";
                    BrandTextBlock.Text = allProducts[i][4].ToString();
                    ImageProduct.Source = new BitmapImage(new Uri(allProducts[i][8].ToString()));
                    DescriptionTextBlock.Text = allProducts[i][2].ToString();
                    CharacteristicsTextBlock.Text = allProducts[i][3].ToString();
                    PriceTextBlock.Text = allProducts[i][5].ToString() + " ₽";
                }
            }
        }
    }
}
