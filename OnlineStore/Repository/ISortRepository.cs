using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
   public interface ISortRepository
    {
        List<Sort> GetAllSortPrices();
    }
}
