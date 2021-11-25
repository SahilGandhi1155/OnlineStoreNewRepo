using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Repository
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        IEnumerable<Category> GetAllCategory();
        Category Add(Category category);
        Category Update(Category categoryChanges);
        Category Delete(int id);
    }
}
