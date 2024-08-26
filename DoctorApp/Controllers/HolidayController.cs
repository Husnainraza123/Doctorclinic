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
        public ActionResult AddHoliday()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddHoliday(Holiday h)
        {
            //r.CreatedDate = DateTime.Now;
            db.Holidays.Add(h);
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Holiday added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the Role." });
            }
        }
    }
}