using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;
using vidly.Models;
using vidlyDbContext;
using Newtonsoft.Json;
using vidlyDbContext.Entities;

namespace vidly.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class ModeratorsController : Controller
    {
        // GET: /Moderators/
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

        public bool AddOrUpdateModerator(vidlyDbContext.Entities.Moderator moderator)
        {
            var flag = false;
            try
            {
                moderator.UserType = "moderator";
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

        public int GetSessionId()
        {
            return (int)Session["UserId"];
        }

        [HttpPost]
        public ActionResult Create(vidlyDbContext.Entities.Moderator moderator)
        {
            if (GetSessionId() != 0)
            {
                if (AddOrUpdateModerator(moderator))
                {
                    return RedirectToAction(logInUrl);
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        public ActionResult ModeratorProfile()
        {
            if (GetSessionId() != 0)
            {
                var sessionId = GetSessionId();
                var moderator = context.Moderators.FirstOrDefault(a => a.Id == sessionId);
                ViewBag.moderator = moderator;

                return View(moderator);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        public ActionResult AddMovie()
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

        [HttpGet]
        public ActionResult AddNewMovie(Movie data)
        {
            if (GetSessionId() != 0)
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
            else
            {
                return RedirectToAction(logInUrl);
            }
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
            if (GetSessionId() != 0)
            {
                //using viewbag
                var sessionId = GetSessionId();
                var moderator = context.Moderators.FirstOrDefault(a => a.Id == sessionId);
                ViewBag.moderator = moderator;
                return View();
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
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

            if (GetSessionId() != 0)
            {
                if (AddOrUpdateModerator(moderator))
                {
                    return RedirectToAction("ModeratorProfile");
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

        public ActionResult BrowseCustomers()
        {
            if (GetSessionId() != 0)
            {
                var customers = context.Customers.ToList();
                return View(customers);
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
                var movie = context.Movies.FirstOrDefault(a => a.Id == id);
                return View(movie);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
            
        }
        public ActionResult EditMovie(int id)
        {
            if (GetSessionId() != 0)
            {
                var movie = context.Movies.FirstOrDefault(a => a.Id == id);
                return View(movie);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
            
        }

        [HttpPost]
        public ActionResult EditMovie(vidlyDbContext.Entities.Movie movie)
        {
            if (GetSessionId() != 0)
            {
                AddOrUpdateMovie(movie);
                return View(movie);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
            
        }

        public ActionResult DeleteMovie(int id)
        {
            if (GetSessionId() != 0)
            {
                var movie = context.Movies.FirstOrDefault(a => a.Id == id);
                return View(movie);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
            
        }

        [HttpPost]
        public ActionResult DeleteMovie(vidlyDbContext.Entities.Movie movie)
        {
            if (GetSessionId() != 0)
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
            else
            {
                return RedirectToAction(logInUrl);
            }

            
        }

        public ActionResult EditCustomer(int id)
        {
            if (GetSessionId() != 0)
            {
                var customer = context.Customers.FirstOrDefault(a => a.Id == id);
                return View(customer);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        [HttpPost]
        public ActionResult EditCustomer(vidlyDbContext.Entities.Customer customer)
        {
            if (GetSessionId() != 0)
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
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        public ActionResult CustomerDetails(int id)
        {
            if (GetSessionId() != 0)
            {
                var customer = context.Customers.FirstOrDefault(a => a.Id == id);
                return View(customer);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
            
        }

        [HttpPost]
        public ActionResult DeleteCustomer(vidlyDbContext.Entities.Customer customer)
        {
            if (GetSessionId() != 0)
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
            else
            {
                return RedirectToAction(logInUrl);
            } 
        }
        public ActionResult DeleteCustomer(int id)
        {
            if (GetSessionId() != 0)
            {
                var customer = context.Customers.FirstOrDefault(a => a.Id == id);
                return View(customer);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        public ActionResult LogOutModerator()
        {
            if (GetSessionId() != 0)
            {
                Session.Abandon();
            }
            return RedirectToAction(logInUrl);
        }
    }
}