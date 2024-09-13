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
        public ActionResult Invoice()
        {
            List<InvoiceViewModel> invoices = db.BrowseInvoice_sp().Select(
                s => new InvoiceViewModel()
                {
                    
                    PatientName = s.Patient_Name,
                    InvoiceID = s.ID,
                    Patient_Address=s.Patient_Address,
                    Billing_Address =s.Billing_Address,
                    InvoiceDate = Convert.ToDateTime(s.Invoice_Date),
                    Item = s.Items,
                    Description=s.Description,
                    UnitCost=s.UnitCost,
                    Quantity=s.Qty,
                    Amount= Convert.ToDecimal(s.Amount),
                    GrandTotal = Convert.ToDecimal(s.GrandTotal)

                }).ToList();
            return View(invoices);
        }



        public ActionResult AddInvoice()
        {
            var row2 = db.Patients.ToList();
            ViewBag.patientnames = row2;

            return View();
        }
        [HttpPost]
        public JsonResult AddInvoice(Invoice i)
        {
            i.CreatedDate = DateTime.Now;
            db.Invoices.Add(i);
            int c = db.SaveChanges();
            if (c > 0)
            {
                return Json(new { success = true, message = "Invoice added successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occurred while adding the Invoice." });
            }
        }

    }
}