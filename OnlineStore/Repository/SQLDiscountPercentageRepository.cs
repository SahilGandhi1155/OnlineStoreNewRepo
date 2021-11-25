using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Repository
{
    public class SQLDiscountPercentageRepository : IDiscountPercentageRepository
    {
        public SQLDiscountPercentageRepository(AppDbContext context)
        {
            this.context = context;
        }
        public AppDbContext context { get; }

        public DiscountPercentage Add(DiscountPercentage discountPercentage)
        {
            throw new NotImplementedException();
        }

        public DiscountPercentage Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DiscountPercentage> GetAllDiscountPercentages()
        {
            return context.DiscountPercentages.ToList();
        }

        public DiscountPercentage GetDiscountPercentage(int id)
        {
            throw new NotImplementedException();
        }

        public DiscountPercentage Update(DiscountPercentage discountPercentageChanges)
        {
            throw new NotImplementedException();
        }
    }
}
