using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Repository
{
    public class SortRepository : ISortRepository
    {
        private List<Sort> _sortList;

        public SortRepository()
        {
            _sortList = new List<Sort>()
            {
                new Sort() {id= 1,Name = "Newest",isSelected=false },
                new Sort() {id= 2,Name = "Low To High",isSelected=false },
                new Sort() {id= 3,Name = "High to Low",isSelected=false },
                new Sort() {id= 4,Name = "Popularity",isSelected=false }
            };
        }

        public List<Sort> GetAllSortPrices()
        {
            return _sortList;
        }
    }
}
