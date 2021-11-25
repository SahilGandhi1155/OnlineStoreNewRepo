using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;

namespace OnlineStore.Repository
{
    public class CustomerDetailRepository : ICustomerDetailRepository
    {
        private AppDbContext context;

        public CustomerDetailRepository(AppDbContext context)
        {
            this.context = context;
        }

        public CustomerDetail Add(CustomerDetail customerDetail)
        {
            context.CustomerDetails.Add(customerDetail);
            context.SaveChanges();
            return customerDetail;
        }

        public CustomerDetail Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerDetail> GetAllCustomers()
        {
            return context.CustomerDetails;
        }

        public CustomerDetail GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerDetail Update(CustomerDetail customerDetailChanges)
        {
            var customerDetails = context.CustomerDetails.Attach(customerDetailChanges);
            customerDetails.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerDetailChanges;
        }
    }
}
