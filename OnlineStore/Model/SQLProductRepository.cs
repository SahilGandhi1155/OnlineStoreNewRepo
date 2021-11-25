using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public class SQLProductRepository : IProductRepository
    {
        public SQLProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public AppDbContext context { get; }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {
           Product product = context.Products.Find(id);
            if(product!=null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public List<Product> GetAllProducts()
        {
            return context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return context.Products.Find(id);
        }

        public List<Product> SearchProducts(string search)
        {
           if(string.IsNullOrEmpty(search))
            {
                return context.Products.ToList();
            }
            else
            {
                return context.Products.Where(e => e.Name.Contains(search)).ToList();
            }
        }

        public Product Update(Product productChanges)
        {
           var product = context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;

        }
    }
}
