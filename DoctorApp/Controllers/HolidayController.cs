using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.IO;


namespace DoctorApp.Controllers
{
    public class HolidayController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Holiday
        public ActionResult Holiday()
        {
            List<Holiday> dc = db.Holidays.ToList();
            return View(dc);
        }
    }
}