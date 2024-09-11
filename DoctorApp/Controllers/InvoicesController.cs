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
    public class InvoicesController : Controller
    {
        DoctorClinicEntities db = new DoctorClinicEntities();
        // GET: Invoices
        public ActionResult Invoices()
        {
            return View();
        }
    }
}