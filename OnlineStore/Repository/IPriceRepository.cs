using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
    public interface IPriceRepository
    {
        Price GetPrice(int id);
        List<Price> GetAllPrices();
        Price Add(Price price);
        Price Update(Price priceChanges);
        Price Delete(int id);
    }
}
