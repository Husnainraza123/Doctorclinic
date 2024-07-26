using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorApp.Controllersl
{
    public class RoleController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Role
        public ActionResult Role()
        {
            List<Role> dc = db.Roles.ToList();
            return View(dc);
        }
        public ActionResult AddRole()
        {
            return View();
        }


        [HttpPost]
        public JsonResult AddRole(Role r)
        {
            r.CreatedDate = DateTime.Now;
            db.Roles.Add(r);
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Role added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the Role." });
            }
        }

        public ActionResult EditRole(int id)
        {
            var row = db.Roles.Where(model => model.RoleID == id).FirstOrDefault();
            return View(row);

        }

        [HttpPost]
        public JsonResult EditRole(Role r)
        {

            db.Entry(r).State = EntityState.Modified;
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
        public ActionResult DeleteRole(int id)
        {
            if (id > 0)
            {
                try
                {
                    var RoleIdRow = db.Roles.FirstOrDefault(model => model.RoleID == id);
                    if (RoleIdRow != null)
                    {
                        db.Entry(RoleIdRow).State = EntityState.Deleted;
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