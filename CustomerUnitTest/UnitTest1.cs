using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rytasoft.CustomerSupport.Web.Controllers;
using Rytasoft.CustomerSupport.Data;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace CustomerUnitTest
{
    public class MockRepository : ICustomerRepository
    {
        List<tblCustomer> customers = new List<tblCustomer>() {
            new tblCustomer() {Id = 23, CustomerEmail = "ryantavan@gmail.com", CusomerAddress = "34 test st", CustomerName="ryan tavan", IsDeleted =false }
        };

        public List<tblCustomer> GetCustomers()
        {
            return customers;
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CustomersAvailable()
        {
            var mockdata = new MockRepository();
            CustomerController cc = new CustomerController(mockdata);
            ViewResult result = cc.Index() as ViewResult;
            List<tblCustomer> custs = result.Model as List<tblCustomer>;
            var firstcust = custs.FirstOrDefault();
            Assert.AreEqual(firstcust.Id, 23);
        }
    }
}
