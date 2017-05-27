using Rytasoft.CustomerSupport.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rytasoft.CustomerSupport.Web.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerRepository repo;
        public CustomerController(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        // GET: Cusomer
        public ActionResult Index()
        {
            if (Session != null && Session["CustomerDetailError"] != null)
            {
                ViewBag.Error = Session["CustomerDetailError"].ToString();
                Session["CustomerDetailError"] = null;
            }

            var customers = repo.GetCustomers();

            return View(customers);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblCustomer model)
        {
            if (ModelState.IsValid)
            {
                using (CustomerSupportDBEntities context = new CustomerSupportDBEntities())
                {
                    model.IsDeleted = false;
                    context.tblCustomers.Add(model);
                    context.SaveChanges();
                }
            }
            else
            {
                ViewBag.Errors = "Model is not valid!";
                return View(model);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            using (CustomerSupportDBEntities context = new CustomerSupportDBEntities())
            {
                var customer = context.tblCustomers.Where(c => c.Id == id).FirstOrDefault();
                if (customer != null)
                {
                    return View(customer);
                }
                Session["CustomerDetailError"] = "No customer with this id is identified";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (CustomerSupportDBEntities context = new CustomerSupportDBEntities())
            {
                var customer = context.tblCustomers.Where(c => c.Id == id).FirstOrDefault();
                if (customer != null)
                {
                    return View(customer);
                }
                else
                {
                    Session["CustomerDetailError"] = "No customer with this id is identified";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(tblCustomer model)
        {
            if (ModelState.IsValid)
            {
                using (CustomerSupportDBEntities context = new CustomerSupportDBEntities())
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else
            {
                ViewBag.Errors = "Model is not valid!";
                return View(model);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (CustomerSupportDBEntities context = new CustomerSupportDBEntities())
            {
                var customer = context.tblCustomers.Where(c => c.Id == id).FirstOrDefault();
                if (customer != null)
                {
                    customer.IsDeleted = true;
                    context.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    Session["CustomerDetailError"] = "No customer with this id is identified";
                }

                return RedirectToAction("Index");
            }
        }
    }
}