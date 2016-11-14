using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LaidigSystemsC.Models;
using System.Web.Security;

namespace LaidigSystemsC.Controllers
{
    public class AccountController : Controller
    {

        private OurDbContext db = new OurDbContext();


        // GET: Account
        public ActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    return View(db.useraccounts.ToList());
                }
            }
            return RedirectToAction("LoggedIn", "Account");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.useraccounts.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();

                ViewBag.Message = account.FirstName + " " + account.LastName + " Successfully Registered !";

            }
            return View();
        }


        //Login

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.useraccounts.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", " UserName or Password is Wrong !");
                }
            }
            return View();
        }

        public ActionResult LoggedIn()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Uploadtypes()
        {
            return View();
        }



        public ActionResult CheckExistingEmail(string email)
        {
            if (db.useraccounts.Any(x => x.Email == email))
            {
                return Json(string.Format("{0} allready exits", email), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult CheckExistingUserName(string username)
        {
            if (db.useraccounts.Any(x => x.UserName == username))
            {
                return Json(string.Format("{0} allready exits", username), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }


    }
}