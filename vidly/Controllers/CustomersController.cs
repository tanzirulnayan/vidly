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
		// GET: /Customers/

		VidlyDbContext context = new VidlyDbContext();
		private string logInUrl = "../Home/Index";
		public ActionResult Index()
		{
			if (GetSessionId() != 0)
			{
				return View();
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
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
			if (GetSessionId() != 0)
			{
				var sessionId = GetSessionId();
				var customer = context.Customers.FirstOrDefault(a => a.Id == sessionId);

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
			else
			{
				return RedirectToAction(logInUrl);
			}
			
		}

		public ActionResult EditProfile()
		{
			if (GetSessionId() != 0)
			{
				var sessionId = GetSessionId();
				var customer = context.Customers.FirstOrDefault(a => a.Id == sessionId);

				return View(customer);
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		[HttpPost]
		public ActionResult EditProfile(vidlyDbContext.Entities.Customer customer)
		{
			if (GetSessionId() != 0)
			{
				if (AddOrUpdateCustomer(customer))
				{
					return RedirectToAction("CustomerProfile");
				}
				else
				{
					return RedirectToAction("EditProfile");
				}
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
			
		}

		public ActionResult BrowseMovies()
		{
			if (GetSessionId() != 0)
			{
				var movies = context.Movies.ToList();
				ViewBag.movies = movies;
				return View(movies);
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		public ActionResult BorrowMovie(int id)
		{
			if (GetSessionId() != 0)
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
				movie.BorrowCount++;

				context.Movies.AddOrUpdate(m => m.Id, movie);
				context.SaveChanges();

				return RedirectToAction("BrowseMovies");
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		public ActionResult MovieDetails(int id)
		{
			if (GetSessionId() != 0)
			{
				var movie = context.Movies.FirstOrDefault(m => m.Id == id);
				return View(movie);
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		public ActionResult AllBorrows()
		{
			if (GetSessionId() != 0)
			{
				var sessionId = GetSessionId();

				var borrows = (from bh in context.BorrowHistories
							   join m in context.Movies on bh.MovieId equals m.Id
							   where bh.CustomerId == sessionId
							   orderby bh.BorrowDate descending
							   select new
							   {
								   MovieName = m.Name,
								   DateOfBorrow = bh.BorrowDate,
								   ReturnDateOfBorrow = bh.ReturnDate,
								   StatusOfBorrow = bh.BorrowStatus
							   }).ToList();
				ViewBag.allBorrows = borrows;
				return View();
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		public ActionResult CurrentBorrows()
		{
			if (GetSessionId() != 0)
			{
			    var sessionId = GetSessionId();
				//current borrow ops to be implemented here
				return View();
			}
			else
			{
				return RedirectToAction(logInUrl);
			}
		}

		public ActionResult LogOutCustomer()
		{
			if (GetSessionId() != 0)
			{
				Session.Abandon();
			}
			return RedirectToAction(logInUrl);
		}
	}
}