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
    public class DoctorController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Doctor
        public ActionResult Doctor()
        {
            List<Doctor> dc = db.Doctors.ToList();
            return View(dc);
        }
        [HttpPost]
        public ActionResult Doctors(Doctor d)
        {
            return View();
        }




        public ActionResult AddDoctor()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddDoctor(Doctor d)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/Doctor/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/Doctor/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    d.ImageFile = string.Format("/assets/DoctorImage/Doctor/{0}", uniquename);
                    d.Image = string.Format("/assets/DoctorImage/Doctor/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Doctors.Add(d);
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


        public ActionResult EditDoctor(int id)
        {
            var row = db.Doctors.Where(model => model.DoctorID == id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]
        public JsonResult EditDoctor(Doctor d)
        {
            try
            {
                // Retrieve existing doctor record from the database
                var existingDoctor = db.Doctors.Find(d.DoctorID);
                if (existingDoctor == null)
                {
                    return Json(new { success = false, message = "Doctor not found." });
                }

                // Handle file upload
                if (Request.Files["ImageFile"] != null)
                {
                    var Imgfile = Request.Files["ImageFile"];
                    if (!string.IsNullOrEmpty(Imgfile.FileName))
                    {
                        string subPath = "/assets/DoctorImage/Doctor";
                        if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                        var uniquename = Guid.NewGuid().ToString() + ext;
                        var rootpath = Server.MapPath(subPath);
                        var iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);

                        d.ImageFile = $"/assets/DoctorImage/Doctor/{uniquename}";
                        d.Image = $"/assets/DoctorImage/Doctor/{uniquename}";

                        Imgfile.SaveAs(iconFileSavePath);

                        // Update fields of the existing doctor
                        existingDoctor.FirstName = d.FirstName;
                        existingDoctor.LastName = d.LastName;
                        existingDoctor.Email = d.Email;
                        existingDoctor.DOB = d.DOB;
                        existingDoctor.Gender = d.Gender;
                        existingDoctor.Address = d.Address;
                        existingDoctor.Country = d.Country;
                        existingDoctor.City = d.City;
                        existingDoctor.Province = d.Province;
                        existingDoctor.Phone = d.Phone;
                        existingDoctor.PostalCode = d.PostalCode;
                        existingDoctor.Description = d.Description;
                        existingDoctor.Image = d.ImageFile;
                        existingDoctor.Image = d.Image;
                        existingDoctor.ModifyBy = d.ModifyBy;
                        existingDoctor.ModifyDate = d.ModifyDate;
                        existingDoctor.Status = d.Status;

                        db.Entry(existingDoctor).State = EntityState.Modified;

                        try
                        {
                            db.SaveChanges();
                            return Json(new { success = true });
                        }
                        catch (Exception ex)
                        {
                            // Handle concurrency exception
                            Console.WriteLine(ex.Message);
                            return Json(new { success = false, message = "Concurrency conflict detected. Please try again." });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json(new { success = false, message = "An error occurred while updating the doctor." });
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    var DoctorIdRow = db.Doctors.FirstOrDefault(model => model.DoctorID == id);
                    if (DoctorIdRow != null)
                    {
                        db.Entry(DoctorIdRow).State = EntityState.Deleted;
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


        public ActionResult DoctorProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Fetch the doctor
            var doctor = db.Doctors.FirstOrDefault(d => d.DoctorID == id);

            if (doctor == null)
            {
                return HttpNotFound();
            }

            // Fetch the doctor's experiences
            var experiences = db.BrowseDoctorExpBy_sp(id).Where(e => e.DoctorID == id).Select(s=> new ExperienceInformation()
            {
                ExperienceID = s.ID,
                DoctorID= s.DoctorID.Value,
                ExperiencePeriod = s.ExperiencePeriod,
                Position = s.Position,
                CompanyName = s.ComName
            }).ToList();
            var educations = db.EducationInformations.Where(e => e.DoctorID == id).ToList();

            // Create and populate the view model
            var viewModel = new DoctorProfileViewModal
            {
                // Map other Doctor properties here
                ExperienceList = experiences,
                Doctor = doctor,
                EducationList = educations
            };

            return View(viewModel);
        }

        public ActionResult AddEducation()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddEducationinformation(EductionViewModel ed)
        {
            db.EducationInformations.Add(new EducationInformation()
            {
                DoctorID = ed.DoctorID,
                CompleteDate = ed.CompleteDate,
                Degree = ed.Degree,
                InstitutionName = ed.InstitutionName,
                StartingDate= ed.StartingDate,
            });
            db.ExperienceInformations.Add(new ExperienceInformation()
            {
                DoctorID = ed.DoctorID,
                CompanyName= ed.CompanyName,
                PeriodFrom = ed.PeriodFrom,
                PeriodTo = ed.PeriodTo,
                Position = ed.Position,
            });

            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(data: 1);
            }
            return Json(data:1,JsonRequestBehavior.AllowGet);
        }

    }
}


