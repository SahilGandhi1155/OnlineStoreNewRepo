using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repository
{
    public interface ICustomerDetailRepository
    {
        CustomerDetail GetCategory(int id);
        IEnumerable<CustomerDetail> GetAllCustomers();
        CustomerDetail Add(CustomerDetail customerDetail);
        CustomerDetail Update(CustomerDetail customerDetailChanges);
        CustomerDetail Delete(int id);
    }
}
