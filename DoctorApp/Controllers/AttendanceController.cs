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

        [HttpPost]
        public JsonResult AddAttendance(Attendance a)
        {
            a.CreatedDate = DateTime.Now;
            db.Attendances.Add(a);
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

        public ActionResult EditAttendance(int id)
        {
            var row = db.BrowseAttendanceByID_sp(id).FirstOrDefault();
            AttendanceViewModel model1 = new AttendanceViewModel()
            {
                AttendanceID = row.ID,
                EmployeeID = (int)row.EmployeeID,
                EmployeeName = row.EmployeeName,
                DOB = Convert.ToDateTime(row.Date),               
                Status = row.Status
            };
            return View(model1);
        }

        [HttpPost]
        public JsonResult EditAttendance(Attendance a)
        {
            db.Entry(a).State = EntityState.Modified;
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Attendance Edit successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the Attendance." });
            }
        }


        [HttpPost]
        public ActionResult DeleteAttendance(int id)
        {
            if (id > 0)
            {
                try
                {
                    var AttendanceIdRow = db.Attendances.FirstOrDefault(model => model.AttendanceID == id);
                    if (AttendanceIdRow != null)
                    {
                        db.Entry(AttendanceIdRow).State = EntityState.Deleted;
                        int a = db.SaveChanges();

                        if (a > 0)
                        {
                            return Json(data: 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.Message);
                }
            }
            return Json(data: 0);

        }
    }
}