using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorApp.Controllers
{
    public class AccountController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        
        public ActionResult Login()
        {
            return View();
        }
        
       

        [HttpPost]
        public ActionResult LoginUser(string usernameOrEmail, string password)
        {
            var user = db.Users.FirstOrDefault(u =>
                (u.UName == usernameOrEmail || u.UEmail == usernameOrEmail) && u.UPass == password);

            if (user != null)
            {
                return Json(new { success = true, message = "Login Successfull" });
                
            }
            else
            {
                return Json(new { success = false, message = "Invalid username or password" });
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login", "AccountController");

        }
    }
}