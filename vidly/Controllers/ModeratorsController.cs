﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI;
using vidly.Models;
using vidly.ViewModels;
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
        private string logInUrl = "../Home/Login";
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
            var sessionId = 0;
            var sessionUserType = "";
            if (Session["UserId"] != null)
            {
                sessionId = (int) Session["UserId"];
            }

            if (Session["UserType"] != null)
            {
                sessionUserType = (string) Session["UserType"];
            }

            if (sessionId != 0 && sessionUserType == "moderator")
            {
                return sessionId;
            }
            else
            {
                return 0;
            }
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

        public ActionResult RentalHistory()
        {
            if (GetSessionId() != 0)
            {
                var borrows = (from bh in context.BorrowHistories
                               join m in context.Movies on bh.MovieId equals m.Id
                               join c in context.Customers on bh.CustomerId equals c.Id
                               orderby bh.BorrowDate descending
                               select new ModeratorRentalHistory()
                               {
                                   BorrowId = bh.Id,
                                   CustomerId = bh.CustomerId,
                                   CustomerName = c.Name,
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

        public ActionResult Rents()
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

        public ActionResult PendingRentals()
        {
            if (GetSessionId() != 0)
            {
                var borrows = (from bh in context.BorrowHistories
                               join m in context.Movies on bh.MovieId equals m.Id
                               join c in context.Customers on bh.CustomerId equals c.Id
                               where bh.BorrowStatus == "pending"
                               orderby bh.BorrowDate descending 
                               select new ModeratorRentalHistory()
                               {
                                   BorrowId = bh.Id,
                                   CustomerId = bh.CustomerId,
                                   CustomerName = c.Name,
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

        public ActionResult ExpiredPendingRentals()
        {
            if (GetSessionId() != 0)
            {
                var borrows = (from bh in context.BorrowHistories
                               join m in context.Movies on bh.MovieId equals m.Id
                               join c in context.Customers on bh.CustomerId equals c.Id
                               where bh.BorrowStatus == "pending" && bh.ReturnDate < DateTime.Today
                               orderby bh.BorrowDate descending 
                               select new ModeratorRentalHistory()
                               {
                                   BorrowId = bh.Id,
                                   CustomerId = bh.CustomerId,
                                   CustomerName = c.Name,
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

        public ActionResult AddRent()
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

        [HttpPost]
        public ActionResult AddRent(vidlyDbContext.Entities.BorrowHistory borrowHistory)
        {
            //implement addRent here
            if (GetSessionId() != 0)
            {
                //borrow ops here using modal
                vidlyDbContext.Entities.BorrowHistory borrow = new BorrowHistory();
                borrow.Id = Guid.NewGuid();
                borrow.MovieId = borrowHistory.MovieId;
                borrow.CustomerId = borrowHistory.CustomerId;
                borrow.BorrowDate = DateTime.Today;
                borrow.ReturnDate = DateTime.Today.AddDays(7);
                borrow.BorrowStatus = "pending";

                context.BorrowHistories.AddOrUpdate(m => m.Id, borrow);
                context.SaveChanges();

                var movie = context.Movies.FirstOrDefault(m => m.Id == borrowHistory.MovieId);
                if (movie != null)
                {
                    movie.BorrowCount++;
                }

                context.Movies.AddOrUpdate(m => m.Id, movie);
                context.SaveChanges();

                return RedirectToAction("PendingRentals");
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

        public ActionResult ReturnMovie()
        {
            if (GetSessionId() != 0)
            {
                var borrows = (from bh in context.BorrowHistories
                               join m in context.Movies on bh.MovieId equals m.Id
                               join c in context.Customers on bh.CustomerId equals c.Id
                               where bh.BorrowStatus == "pending"
                               orderby bh.BorrowDate ascending 
                               select new ModeratorRentalHistory()
                               {
                                   BorrowId = bh.Id,
                                   CustomerId = bh.CustomerId,
                                   CustomerName = c.Name,
                                   MovieName = m.Name,
                                   DateOfBorrow = bh.BorrowDate,
                                   ReturnDateOfBorrow = bh.ReturnDate,
                                   StatusOfBorrow = bh.BorrowStatus
                               }).ToList();

                ViewBag.allBorrows = borrows;
                return View(borrows);
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }
        public ActionResult ReturnMovieToDb(Guid id)
        {

            if (GetSessionId() != 0)
            {
                var borrow = context.BorrowHistories.FirstOrDefault(m => m.Id == id);
                if (borrow != null)
                {
                    borrow.BorrowStatus = "returned";
                    context.BorrowHistories.AddOrUpdate(m => m.Id, borrow);
                    context.SaveChanges();
                }
                return RedirectToAction("ReturnMovie");
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }

        public ActionResult CancelRent(Guid id)
        {
            if (GetSessionId() != 0)
            {
                var rent = context.BorrowHistories.FirstOrDefault(a => a.Id == id);
                if (rent != null)
                {
                    context.BorrowHistories.Remove(rent);
                    context.SaveChanges();
                }
                return RedirectToAction("ReturnMovie");
            }
            else
            {
                return RedirectToAction(logInUrl);
            }
        }
    }
}