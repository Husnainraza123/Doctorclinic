using DoctorApp.Models;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DoctorApp.Controllers
{
    public class AppointmentController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();

        // GET: Appointment
        public ActionResult Appointment()
        {
            List<AppointmentViewModel> appointments = db.BrowseAppointment_sp().Select(
                s => new AppointmentViewModel()
                {
                    Image = s.Image,
                    PatientName = s.PatientName,
                    Age = s.Age,
                    Status = s.Status,
                    AppointmentID = s.ID,
                    AppointmentTime = s.AppointmentTime,
                    DoctorName = s.DoctorName,
                    DOB = Convert.ToDateTime(s.DOB),
                    DepartmentsName = s.DepartmentsName,
                    EndTime = s.EndTime
                }).ToList();
            return View(appointments);

            //List<Appointment> dc = appointment;
            //return View(appointment);
        }

        public ActionResult AddAppointment()
        {
            var row1 = db.Doctors.ToList();
            ViewBag.doctornames = row1;

            var row2 = db.Patients.ToList();
            ViewBag.patientnames = row2;

            var row3 = db.Departments.ToList();
            ViewBag.Department = row3;

            return View();
        }

        [HttpPost]
        public JsonResult AddAppointment(Appointment a)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/AppointmentImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/AppointmentImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    a.ImageFile = string.Format("/assets/DoctorImage/AppointmentImage/{0}", uniquename);
                    a.Image = string.Format("/assets/DoctorImage/AppointmentImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Appointments.Add(a);
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

        public ActionResult EditAppointment(int id)
        {
            var row = db.BrowseAppointmentByID_sp(id).FirstOrDefault();
            AppointmentViewModel model1 = new AppointmentViewModel()
            {
                AppointmentID = row.ID,
                Image = row.Image,
                PatientName = row.PatientName,
                Age = (int)row.Age,
                DoctorName = row.DoctorName,
                DepartmentsName = row.DepartmentsName,
                DOB = (DateTime)row.Date,
                Time = row.AppointmentTime,
                EndTime= row.EndTime.ToString(),
               
                Status= true,

            };
            return View(model1);
        }




        [HttpPost]
        public JsonResult EditAppointment(Appointment a)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/AppointmentImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/AppointmentImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    a.ImageFile = string.Format("/assets/DoctorImage/AppointmentImage/{0}", uniquename);
                    a.Image = string.Format("/assets/DoctorImage/AppointmentImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Entry(a).State = EntityState.Modified;
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
        public ActionResult DeleteAppointment(int id)
        {
            if (id > 0)
            {
                try
                {
                    var AppointmentIdRow = db.Appointments.FirstOrDefault(model => model.AppointmentID == id);
                    if (AppointmentIdRow != null)
                    {
                        db.Entry(AppointmentIdRow).State = EntityState.Deleted;
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