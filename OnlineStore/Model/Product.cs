using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class Product
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public int OriginalPrice { get; set; }

        public int DiscountPercentage { get; set; }

        public int DiscountPrice { get; set; }

        public String Image { get; set; }

        public String Description { get; set; }

        public int CategoryId { get; set; }

        public String BrandName { get; set; }

        public int BrandId { get; set; }

        public int brandId { get; set; }

        public int catId { get; set; }
       
        public int subCatId { get; set; }

        



    }
}
