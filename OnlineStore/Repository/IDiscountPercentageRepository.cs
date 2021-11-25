
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
   public interface IDiscountPercentageRepository
    {
        DiscountPercentage GetDiscountPercentage(int id);
        List<DiscountPercentage> GetAllDiscountPercentages();
        DiscountPercentage Add(DiscountPercentage discountPercentage);
        DiscountPercentage Update(DiscountPercentage discountPercentageChanges);
        DiscountPercentage Delete(int id);
    }
}
