using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using vidlyDbContext;
using Customer = vidly.Models.CustomerViewModel;

namespace vidly.Controllers
{
	public class CustomersController : Controller
	{
		//
		// GET: /Customers/

		VidlyDbContext context = new VidlyDbContext();

		public ActionResult Index()
		{
			var customers = context.Customers.ToList();
			ViewBag.customers = customers;
			return View();
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(vidlyDbContext.Entities.Customer customer)
		{
			if (AddOrUpdateCustomer(customer))
			{
				return RedirectToAction("../Home/Index");
			}
			else
			{
				return RedirectToAction("Create");
			}
		}

		public bool AddOrUpdateCustomer(vidlyDbContext.Entities.Customer customer)
		{
			var flag = false;
			try
			{
				if (customer.Id == 0)
					customer.UserType = "customer";

				context.Customers.AddOrUpdate(m => m.Id, customer);
				context.SaveChanges();
				flag = true;
			}
			catch (Exception exception)
			{
				   
			}           
			return flag;
		}

		public ActionResult CustomerProfile()
		{

			var customer = context.Customers.FirstOrDefault(a => a.Id == 12);

			//var customer = context.Customers.Where(a => a.Id ==6).Select(p => new {p.Id, p.Name});;

			//var banani = (from x in context.Customers
			//              where x.Address == "Banani" || x.Address == "Gulshan"
			//              select new
			//              {
			//                  Name = x.Name,
			//                  Password = x.Password
			//              }).ToList();


			//objDataContext.employees.Find(empId);
			ViewBag.customers = customer;
			return View();
		}


		//[HttpPost]
		//public ActionResult Create1(string MMM)
		//{
		//    return RedirectToAction("Index");
		//}

		public ActionResult EditProfile()
		{
			//using object
			var customer = context.Customers.FirstOrDefault(a => a.Id == 6);
			
			return View(customer);
		}

		[HttpPost]
		public ActionResult EditProfile(vidlyDbContext.Entities.Customer customer)
		{
			//if (customer.Id != 0)
			//{
			//    context.Customers.AddOrUpdate(m => m.Id, customer);
			//    context.SaveChanges();
			//}

			if (AddOrUpdateCustomer(customer))
			{
				return RedirectToAction("CustomerProfile");
			}
			else
			{
				return RedirectToAction("EditProfile");
			}
		}
	}
}