using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using vidly.Models;
using vidlyDbContext;
using Newtonsoft.Json;
using vidlyDbContext.Entities;

namespace vidly.Controllers
{
    public class ModeratorsController : Controller
    {
        //
        // GET: /Moderators/
        VidlyDbContext context = new VidlyDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public bool AddOrUpdateModerator(vidlyDbContext.Entities.Moderator moderator)
        {
            var flag = false;
            try
            {
                if (moderator.Id == 0)
                {
                    moderator.UserType = "moderator";
                }

                context.Moderators.AddOrUpdate(m => m.Id, moderator);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
            }
            return flag;
        }

        public bool AddOrUpdateMovie(vidlyDbContext.Entities.Movie movie)
        {
            var flag = false;
            try
            {
                if (movie.Id == 0)
                {
                    movie.BorrowCount = 0;
                }
                context.Movies.AddOrUpdate(m => m.Id, movie);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {

                flag = false;
            }
            return flag;
        }

        public bool AddOrUpdateCustomer(vidlyDbContext.Entities.Customer customer)
        {
            var flag = false;
            try
            {
                if (customer.Id == 0)
                {
                    customer.UserType = "customer";
                }
                customer.UserType = "customer";
                context.Customers.AddOrUpdate(m => m.Id, customer);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {

                flag = false;
            }
            return flag;
        }

        [HttpPost]
        public ActionResult Create(vidlyDbContext.Entities.Moderator moderator)
        {
            //moderator.UserType = "moderator";
            //context.Moderators.Add(moderator);
            //context.SaveChanges();
            //return RedirectToAction("../Home/Index");

            if (AddOrUpdateModerator(moderator))
            {
                return RedirectToAction("../Home/Index");
            }
            else
            {
                return RedirectToAction("Create");
            }
        }

        public ActionResult ModeratorProfile()
        {
            var moderator = context.Moderators.FirstOrDefault(a => a.Id == 1);
            ViewBag.moderator = moderator;

            return View(moderator);
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        

        [HttpGet]
        public ActionResult AddNewMovie(Movie data)
        {
            var res = "";
            //var movie = JsonConvert.DeserializeObject<Movie>(id);


            try
            {
                context.Movies.Add(data);
                context.SaveChanges();
                res = "Movie added";
            }
            catch (Exception e)
            {
                res = "Something went wrong";
            }
            return Json(new { message = res }, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult EditMovie()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult EditMovie(vidlyDbContext.Entities.Movie movie)
        //{
        //    try
        //    {
        //        //update movie ops
        //        context.SaveChanges();
        //        //show alert here
        //    }
        //    catch (Exception exception)
        //    {

        //        throw exception; //inside an alert
        //    }
        //    return View();
        //}

        public ActionResult EditProfile()
        {
            //using viewbag
            var moderator = context.Moderators.FirstOrDefault(a => a.Id == 1);
            ViewBag.moderator = moderator;
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(vidlyDbContext.Entities.Moderator moderator)
        {
            //if (moderator.Id != 0)
            //{
            //    context.Moderators.AddOrUpdate(m => m.Id, moderator);
            //    context.SaveChanges();
            //}
            //else
            //{
            //    moderator.UserType = "moderator";
            //    context.Moderators.Add(moderator);
            //    context.SaveChanges();
            //}

            //return RedirectToAction("ModeratorProfile");
            if (AddOrUpdateModerator(moderator))
            {
                return RedirectToAction("ModeratorProfile");
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

        public ActionResult BrowseCustomers()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }

        public ActionResult MovieDetails(int id)
        {
            var movie = context.Movies.FirstOrDefault(a => a.Id == id);
            return View(movie);
        }
        public ActionResult EditMovie(int id)
        {
            var movie = context.Movies.FirstOrDefault(a => a.Id == id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult EditMovie(vidlyDbContext.Entities.Movie movie)
        {
            AddOrUpdateMovie(movie);
            return View(movie);
        }

        public ActionResult DeleteMovie(int id)
        {
            var movie = context.Movies.FirstOrDefault(a => a.Id == id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult DeleteMovie(vidlyDbContext.Entities.Movie movie)
        {
            var movieFromDb = context.Movies.FirstOrDefault(x => x.Id == movie.Id);

            if (movieFromDb != null)
            {
                context.Movies.Remove(movieFromDb);
                context.SaveChanges();
                return RedirectToAction("BrowseMovies");
            }
            else
            {
                return View();
            }
        }

        public ActionResult EditCustomer(int id)
        {
            var customer = context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult EditCustomer(vidlyDbContext.Entities.Customer customer)
        {
            if (AddOrUpdateCustomer(customer))
            {
                return RedirectToAction("BrowseCustomers");
            }
            else
            {
                return View(customer);
            }
            
        }

        public ActionResult CustomerDetails(int id)
        {
            var customer = context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(vidlyDbContext.Entities.Customer customer)
        {
            var customerFromDb = context.Customers.FirstOrDefault(x => x.Id == customer.Id);

            if (customerFromDb != null)
            {
                context.Customers.Remove(customerFromDb);
                context.SaveChanges();
                return RedirectToAction("BrowseCustomers");
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeleteCustomer(int id)
        {
            var customer = context.Customers.FirstOrDefault(a => a.Id == id);
            return View(customer);
        }

    }
}