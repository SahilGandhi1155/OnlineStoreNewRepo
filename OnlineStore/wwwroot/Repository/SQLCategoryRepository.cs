using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        public SQLCategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public AppDbContext context { get; }
    
        public Category Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public Category Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return context.Categories;
        }

        

        public Category GetCategory(int id)
        {
            return context.Categories.Find(id);
        }

        public Category Update(Category categoryChanges)
        {
            var category = context.Categories.Attach(categoryChanges);
            category.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return categoryChanges;

        }
    }
}
