using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    internal class productmodel
    {
        public string Product_name { get; set; }
        public string Product_description { get; set; }
        public string Product_characteristics { get; set; }
        public int Brand_ID { get; set; }
        public string Path_to_image { get; set; }
        public decimal Product_price { get; set; }
        public int Category_ID { get; set; }
        public int Product_in_stock { get; set; }
    }
}
