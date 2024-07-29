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
    public class EmployeeController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Employee
        public ActionResult Employee()
        {
            List<EmployeeViewModel> dc = db.BrowseEmployee_sp().Select(x =>
                new EmployeeViewModel()
                {
                    Image = x.Image,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    EmployeeID = x.ID,
                    Email = x.Email,
                    Phone = x.Phone,
                    DOB = Convert.ToDateTime(x.JoiningDate),
                    RoleID = x.RoleID,
                    RoleName = x.RoleName,
                    Status = x.Status
                }
                ).ToList();
            return View(dc);
        }
        public ActionResult AddEmployee()
        {
            var row1 = db.Roles.ToList();
            ViewBag.RoleName = row1;

           

            return View();
        }
        [HttpPost]
        public JsonResult AddEmployee(Employee e)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/EmpImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/EmpImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    e.ImageFile = string.Format("/assets/DoctorImage/EmpImage/{0}", uniquename);
                    e.Image = string.Format("/assets/DoctorImage/EmpImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Employees.Add(e);
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



        public ActionResult EditEmployee(int id)
        {
            var row1 = db.Roles.ToList();
            ViewBag.RoleName = row1;

            var row = db.BrowseEmployeeByID_sp(id).FirstOrDefault();
            EmployeeViewModel model1 = new EmployeeViewModel()
            {
                EmployeeID = row.ID,
                Image = row.Image,
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                Phone = row.Phone,
                DOB = Convert.ToDateTime(row.JoiningDate),
                RoleName = row.RoleName,
                Status = row.Status

            };
            return View(model1);
        }

        [HttpPost]
        public JsonResult EditEmployee(Employee e)
        {
            if (Request.Files["ImageFile"] != null)
            {
                var uniquename = string.Empty;
                var Imgfile = Request.Files["ImageFile"];

                if (Imgfile.FileName != "")
                {
                    string subPath = string.Format("/assets/DoctorImage/EmpImage/");
                    bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

                    if (!exists)
                        System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                    var ext = System.IO.Path.GetExtension(Imgfile.FileName);
                    uniquename = Guid.NewGuid().ToString() + ext;

                    var rootpath = Server.MapPath(string.Format("/assets/DoctorImage/EmpImage/"));
                    string iconFileSavePath = System.IO.Path.Combine(rootpath, uniquename);
                    e.ImageFile = string.Format("/assets/DoctorImage/EmpImage/{0}", uniquename);
                    e.Image = string.Format("/assets/DoctorImage/EmpImage/{0}", uniquename);

                    Imgfile.SaveAs(iconFileSavePath);
                    db.Entry(e).State = EntityState.Modified;
                    int a = db.SaveChanges();
                    if (a > 0)
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
        public ActionResult DeleteEmployee(int id)
        {
            if (id > 0)
            {
                try
                {
                    var EmployeeIdRow = db.Employees.FirstOrDefault(model => model.EmployeeID == id);
                    if (EmployeeIdRow != null)
                    {
                        db.Entry(EmployeeIdRow).State = EntityState.Deleted;
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