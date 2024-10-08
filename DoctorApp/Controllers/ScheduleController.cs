﻿using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DoctorApp.Controllers
{
    public class ScheduleController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Schedule
        public ActionResult Schedule()
        {
            List<ScheduleViewModel> schedules = db.BrowseSchedule_sp().Select(
                 s => new ScheduleViewModel()
                 {
                     ScheduleID = s.ID,
                     Image = s.Image,
                     Days=s.Days,
                     DoctorName = s.DoctorName,
                     DepartmentsName = s.DepartmentsName,
                     StartTime = Convert.ToDateTime(s.AvailableTime),
                     EndTime = Convert.ToDateTime( s.EndTime),
                     Status = s.Status
                 }).ToList();
            return View(schedules);
        }
        public ActionResult AddSchedule()
        {
            var row1 = db.Doctors.ToList();
            ViewBag.doctornames = row1;

            var row3 = db.Departments.ToList();
            ViewBag.Department = row3;

            return View();
        }
        [HttpPost]
        public JsonResult AddSchedule(Schedule s)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/ScheduleImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/ScheduleImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    s.ImageFile = string.Format("/assets/DoctorImage/ScheduleImage/{0}", uniquename);
                    s.Image = string.Format("/assets/DoctorImage/ScheduleImage/{0}", uniquename);
                    //s.StartTime = Convert.ToDateTime("01/01/2024 12:00:00 AM");
                    Imgfile.SaveAs(iconFileSavePath);
                    db.Schedules.Add(s);
                    int c = db.SaveChanges();
                    if (c > 0)
                    {
                        return Json(data: 1);
                    }
                }
            }

            else
            {
                return Json(new { data = 0, error = "No file uploaded" });
            }

            return Json(new { data = 0 });
        }


        public ActionResult EditSchedule(int id)
        {
            var row = db.BrowseScheduleByID_sp(id).FirstOrDefault();
            ScheduleViewModel model1 = new ScheduleViewModel()
            {
                ScheduleID = row.ID,
                Image = row.Image,
                Days = row.Days,
                DoctorName = row.DoctorName,
                DepartmentsName = row.DepartmentsName,
                StartTime = Convert.ToDateTime(row.AvailableTime),
                EndTime = Convert.ToDateTime(row.EndTime),
                Status = row.Status

            };
            return View(model1);
        }



        [HttpPost]
        public JsonResult EditSchedule(Schedule s)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/ScheduleImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/ScheduleImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    s.ImageFile = string.Format("/assets/DoctorImage/ScheduleImage/{0}", uniquename);
                    s.Image = string.Format("/assets/DoctorImage/ScheduleImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Entry(s).State = EntityState.Modified;
                    int c = db.SaveChanges();
                    if (c > 0)
                    {
                        return Json(data: 1);
                    }
                }
            }

            else
            {
                return Json(new { data = 0, error = "No file uploaded" });
            }

            return Json(new { data = 0 });
        }



        [HttpPost]
        public ActionResult DeleteSchedule(int id)
        {
            if (id > 0)
            {
                try
                {
                    var ScheduleIdRow = db.Schedules.FirstOrDefault(model => model.ScheduleID == id);
                    if (ScheduleIdRow != null)
                    {
                        db.Entry(ScheduleIdRow).State = EntityState.Deleted;
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