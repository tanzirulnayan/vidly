using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Microsoft.Ajax.Utilities;
using vidlyDbContext;
using vidlyDbContext.Entities;
//using Customer = vidly.Models.CustomerViewModel;

namespace vidly.Controllers
{
	[SessionState(SessionStateBehavior.Required)]
	public class CustomersController : Controller
	{
		//
		// GET: /Customers/

		VidlyDbContext context = new VidlyDbContext();

		public ActionResult Index()
		{
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

		public int GetSessionId()
		{
			return (int)Session["UserId"];
		}

		public bool AddOrUpdateCustomer(vidlyDbContext.Entities.Customer customer)
		{
			var flag = false;
			try
			{
				//if (customer.Id == 0)
				//{
				//    customer.UserType = "customer";
				//}
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

			//var sessionId = (int)Session["UserId"];
			var customer = context.Customers.FirstOrDefault(a => a.Id == GetSessionId());

			//var test = context.Customers.Include("Movies").Where(x => x.Id == 12).SelectMany()
		   
			//var customer = context.Customers.Where(a => a.Id ==6).Select(p => new {p.Id, p.Name});;

			//var banani = (from x in context.Customers
			//              where x.Address == "Banani" || x.Address == "Gulshan"
			//              select new
			//              {
			//                  Name = x.Name,
			//                  Password = x.Password
			//              }).ToList();


			//objDataContext.employees.Find(empId);
			//ViewBag.customers = customer;
			return View(customer);
		}


		//[HttpPost]
		//public ActionResult Create1(string MMM)
		//{
		//    return RedirectToAction("Index");
		//}

		public ActionResult EditProfile()
		{
			//var sessionId = (int)Session["UserId"];
			var customer = context.Customers.FirstOrDefault(a => a.Id == GetSessionId());
			
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

		public ActionResult BrowseMovies()
		{
			var movies = context.Movies.ToList();
			ViewBag.movies = movies;
			return View(movies);
		}

		public ActionResult BorrowMovie(int id)
		{
			//borrow ops here using modal
			vidlyDbContext.Entities.BorrowHistory borrow = new BorrowHistory();
			//var sessionId = (int) Session["UserId"];
			borrow.Id = Guid.NewGuid();
			borrow.MovieId = id;
			borrow.CustomerId = GetSessionId();
			borrow.BorrowDate = DateTime.Today;
			borrow.ReturnDate = DateTime.Today.AddDays(7);
			borrow.BorrowStatus = "pending";

			context.BorrowHistories.AddOrUpdate(m => m.Id, borrow);
			context.SaveChanges();

			var movie = context.Movies.FirstOrDefault(m => m.Id == id);
			movie.BorrowCount ++;

			context.Movies.AddOrUpdate(m => m.Id, movie);
			context.SaveChanges();

			return RedirectToAction("BrowseMovies");
		}

		public ActionResult MovieDetails(int id)
		{
			var movie = context.Movies.FirstOrDefault(m => m.Id == id);
			return View(movie);
		}

		public ActionResult AllBorrows()
		{
			var sessionId = (int)Session["UserId"];
			//var borrows = context.BorrowHistories.ToList();


			//need to create a list
			var borrows = from bh in context.BorrowHistories
				join m in context.Movies on bh.MovieId equals m.Id
				where bh.CustomerId == GetSessionId()
				select new
				{
					m.Name,
					bh.BorrowDate,
					bh.ReturnDate,
					bh.BorrowStatus
				};
			return View();
		}

		public ActionResult CurrentBorrows()
		{
			var sessionId = (int)Session["UserId"];
			//var borrows = context.BorrowHistories.ToList();
			return View();
		}


		public ActionResult LogOutCustomer()
		{
			Session.Abandon();
			return RedirectToAction("../Home/Login");
		}
	}
}