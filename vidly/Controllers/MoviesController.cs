using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using vidly.Models;
using vidly.ViewModels;
using vidlyDbContext;

namespace vidly.Controllers
{
	public class MoviesController : Controller
	{
		//
		// GET: /Movies/Random

		VidlyDbContext context = new VidlyDbContext();

		//public ActionResult Random()
		//{
		//    var movie = new vidlyDbContext.Movie();
		//    var customers = new List<vidlyDbContext.Customer>
		//    {
		//        new vidlyDbContext.Customer {Name = "Customer 1"},
		//        new vidlyDbContext.Customer {Name = "Customer 2"},
		//        new vidlyDbContext.Customer {Name = "Customer 3"}
		//    };

		//    var viewModel = new RandomMovieViewModel
		//    {
		//        Movie = movie,
		//        Customers = customers
		//    };

		//    return View(viewModel);

		//    return Content("Hello World");
		//    return HttpNotFound();
		//    return new EmptyResult();
		//    return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
		//}

		//[Route("movies/released/{year}/{month:regex(\\d{2}): range(1, 12)}")]

		public ActionResult Edit(int Id)
		{
			return Content("id = " + Id);
		}

		//public ActionResult Index(int? pageIndex, string sortBy)
		//{
		//    if (!pageIndex.HasValue)
		//    {
		//        pageIndex = 1;
		//    }

		//    if (String.IsNullOrWhiteSpace(sortBy))
		//    {
		//        sortBy = "Name";
		//    }

		//    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
		//}

		public ActionResult ByReleaseDate(int year, int month)
		{
			return Content(year + "/" + month);
		}

		public ActionResult Index()
		{
			var movies = context.Movies.ToList();
			ViewBag.movies = movies;

			return View();
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(vidlyDbContext.Entities.Movie movie)
		{
			context.Movies.Add(movie);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public ActionResult MovieList()
		{
			var movies = context.Movies.ToList();
			ViewBag.movies = movies;

			return View();
		}
	}
}