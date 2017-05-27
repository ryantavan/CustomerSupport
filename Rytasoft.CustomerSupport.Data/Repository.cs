using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rytasoft.CustomerSupport.Data
{
    public class CustomerRepository: ICustomerRepository
    {
        private CustomerSupportDBEntities context;
        public List<tblCustomer> GetCustomers()
        {
            context = new CustomerSupportDBEntities();
            return context.tblCustomers.Where(x => x.IsDeleted == false).ToList();
        }
    }
}
