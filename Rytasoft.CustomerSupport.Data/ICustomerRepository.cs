using System.Collections.Generic;

namespace Rytasoft.CustomerSupport.Data
{
    public interface ICustomerRepository
    {
        List<tblCustomer> GetCustomers();
    }
}