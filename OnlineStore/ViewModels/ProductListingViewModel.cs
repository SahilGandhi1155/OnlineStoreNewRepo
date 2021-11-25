using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    public class ProductListingViewModel
    {
        public List<Product> product { get; set; }
        public List<Price>  price { get; set; }
        public List<DiscountPercentage> discountPercentage { get; set; }
        public List<Sort> sorts { get; set; }
    }

    public class FilterProductListingViewModel
    {
        public List<Product> product { get; set; }
        
    }
}
