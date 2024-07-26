using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorApp.Controllers
{
    public class DepartmentController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Department
        public ActionResult Department()
        {
            List<Department> dc = db.Departments.ToList();
            return View(dc);
        }

        public ActionResult AddDepartment()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddDepartment(Department d)
        {
            d.CreatedDate = DateTime.Now;
            db.Departments.Add(d);
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Department added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the department." });
            }
        }


        public ActionResult EditDepartment(int id)
        {
            var row = db.Departments.Where(model => model.DepartmentsID == id).FirstOrDefault();
            return View(row);

        }
        [HttpPost]
        public JsonResult EditDepartment(Department d)
        {

            db.Entry(d).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
                return Json(data: 1);
            }
            else
            {
                return Json(data: 0);

            }

        }

        [HttpPost]
        public ActionResult DeleteDepartment(int id)
        {
            if (id > 0)
            {
                try
                {
                    var DepartmentIdRow = db.Departments.FirstOrDefault(model => model.DepartmentsID == id);
                    if (DepartmentIdRow != null)
                    {
                        db.Entry(DepartmentIdRow).State = EntityState.Deleted;
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


