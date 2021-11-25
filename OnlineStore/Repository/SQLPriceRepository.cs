using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Repository
{
    public class SQLPriceRepository : IPriceRepository
    {
        public SQLPriceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public AppDbContext context { get; }

        public Price Add(Price price)
        {
            throw new NotImplementedException();
        }

        public Price Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Price> GetAllPrices()
        {
            return context.Prices.ToList();
        }

        public Price GetPrice(int id)
        {
            throw new NotImplementedException();
        }

        public Price Update(Price priceChanges)
        {
            throw new NotImplementedException();
        }
    }
}
