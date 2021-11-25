using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Model
{
    public interface IProductRepository
    {
        List<Product> SearchProducts(String search);
        Product GetProduct(int id);
        List<Product> GetAllProducts();
        Product Add(Product product);
        Product Update(Product productChanges);
        Product Delete(int id);

    }
}
