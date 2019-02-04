using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using vidlyDbContext;
using System.Data.Entity.Migrations;


namespace vidly.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        VidlyDbContext context = new VidlyDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ModeratorSignUp()
        {
            return View();
        }

        public ActionResult CustomerSignUp()
        {
            return View();
        }

        public bool AddOrUpdateModerator(vidlyDbContext.Entities.Moderator moderator)
        {
            var flag = false;
            try
            {
                if (moderator.Id == 0)
                    moderator.UserType = "moderator";

                context.Moderators.AddOrUpdate(m => m.Id, moderator);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {

            }
            return flag;
        }

        [HttpPost]
        public ActionResult ModeratorSignUp(vidlyDbContext.Entities.Moderator moderator)
        {
            var flag = false;
            try
            {
                moderator.UserType = "moderator";
                context.Moderators.Add(moderator);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {

                flag = false;
            }
            if (flag == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ModeratorSignUp");
            }
        }

        [HttpPost]
        public ActionResult CustomerSignUp(vidlyDbContext.Entities.Customer customer)
        {
            var flag = false;
            try
            {
                customer.UserType = "customer";
                context.Customers.Add(customer);
                context.SaveChanges();
                flag = true;
            }
            catch (Exception exception)
            {

                flag = false;
            }
            if (flag == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CustomerSignUp");
            }
        }

        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            string redirectString = "Login";
            if (login.UserType == "customer")
            {
                try
                {
                    var customer = context.Customers.First(a => a.Id == login.Id);
                    if (login.Id == customer.Id && login.Password == customer.Password)
                    {
                        Session["UserId"] = customer.Id;
                        Session["UserName"] = customer.Name;
                        redirectString = "../Customers/Index";
                    }
                }
                catch (Exception exception)
                {
                    
                }
            }
            else if (login.UserType == "moderator")
            {
                try
                {
                    var moderator = context.Moderators.First(a => a.Id == login.Id);
                    if (login.Id == moderator.Id && login.Password == moderator.Password)
                    {
                        Session["UserId"] = moderator.Id;
                        Session["UserName"] = moderator.Name;
                        redirectString = "../Moderators/Index";
                    }
                }
                catch (Exception exception)
                {
                    
                    
                }
            }
            return RedirectToAction(redirectString);
        }
    }
}