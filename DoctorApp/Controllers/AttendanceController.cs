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
    public class AttendanceController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Attendance
        public ActionResult Attendance()
        {
            List<AttendanceViewModel> attendances = db.BrowseAttendance_sp().Select(
                   s => new AttendanceViewModel()
                   {
                       AttendanceID = s.ID,
                       EmployeeName = s.EmployeeName,
                       DOB = Convert.ToDateTime(s.date),
                       Status = s.Status
                   }).ToList();
            return View(attendances);
        }
        public ActionResult AddAttendance()
        {
            var row = db.Employees.ToList();
            ViewBag.employeenames = row;
            return View();
        }
    }
}