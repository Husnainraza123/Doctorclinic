using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoctorApp.Controllers
{
    public class LeaveController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Leave
        public ActionResult Leave()
        {
            List<LeaveViewModel> leaves = db.BrowseLeave__sp().Select(
                  l => new LeaveViewModel()
                  {
                      LeaveID = l.ID,
                      EmployeeName = l.EmployeeName,
                      FromDate = Convert.ToDateTime(l.FromDate),
                      ToDate = Convert.ToDateTime(l.ToDate),
                      Days=l.Days,
                      Reason = l.Reason,
                      Status = l.Status
                  }).ToList();
            return View(leaves);
        }
        public ActionResult AddLeave()
        {
            var row = db.Employees.ToList();
            ViewBag.employeenames = row;

            return View();
        }
        [HttpPost]
        public JsonResult AddLeave(Leave_ l)
        {
            l.CreatedDate = DateTime.Now;
            db.Leave_.Add(l);
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Leave added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the Leave." });
            }
        }
    }
}