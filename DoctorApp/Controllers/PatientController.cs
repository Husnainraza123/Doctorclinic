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
    public class PatientController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Patient
        public ActionResult Patient()
        {
            List<Patient> dc = db.Patients.ToList();
            return View(dc);
        }
       

        public ActionResult AddPatient()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddPatient(Patient p)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/PateintImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/PateintImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    p.ImageFile = string.Format("/assets/DoctorImage/PateintImage/{0}", uniquename);
                    p.Image = string.Format("/assets/DoctorImage/PateintImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Patients.Add(p);
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

        public ActionResult EditPatient(int id)
        {
            var row = db.Patients.Where(model => model.PatientsID == id).FirstOrDefault();
            return View(row);

        }


        [HttpPost] // Handle GET requests

        public JsonResult EditPatient(Patient p)
        {
            try
            {

                if (Request.Files["ImageFile"] != null)
                {
                    var uniquename = string.Empty;
                    var Imgfile = Request.Files["ImageFile"];

                    if (Imgfile.FileName != "")
                    {
                        string subPath = string.Format("/assets/DoctorImage/PateintImage/");
                        bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                        if (!exists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                        uniquename = Guid.NewGuid().ToString() + ext;

                        var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/PateintImage/"));
                        string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                        p.ImageFile = string.Format("/assets/DoctorImage/PateintImage/{0}", uniquename);
                        p.Image = string.Format("assets/DoctorImage/PateintImage/{0}", uniquename);

                        Imgfile.SaveAs(iconFileSavePath);
                        db.Entry(p).State = EntityState.Modified;
                        int a = db.SaveChanges();
                        if (a > 0)
                        {
                            return Json(data: 1);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Json(data: 0);

        }




        [HttpPost]
        public ActionResult DeletePatient(int id)
        {
            if (id > 0)
            {
                try
                {
                    var PatientIdRow = db.Patients.FirstOrDefault(model => model.PatientsID == id);
                    if (PatientIdRow != null)
                    {
                        db.Entry(PatientIdRow).State = EntityState.Deleted;
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