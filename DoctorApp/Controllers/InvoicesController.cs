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
        public ActionResult Invoice()
        {
            List<InvoiceViewModel> invoices = db.BrowseInvoice_sp().Select(
                s => new InvoiceViewModel()
                {
                    
                    InvoiceID = s.ID,
                    PatientName = s.Patient_Name,                    
                    Patient_Address = s.Patient_Address,
                    Billing_Address = s.Billing_Address,
                    InvoiceDate = Convert.ToDateTime(s.Invoice_Date),
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


        public ActionResult EditInvoice(int id)
        {
            var row = db.BrowseInvoiceByID_sp(id).FirstOrDefault();
            InvoiceViewModel model1 = new InvoiceViewModel()
            {
                InvoiceID = row.ID,
                PatientName = row.Patient_Name,
                Patient_Address = row.Patient_Address,
                Billing_Address = row.Billing_Address,
                InvoiceDate = Convert.ToDateTime(row.Invoice_Date),
                //Item = row.Items,
                //Description = row.Description,
                //UnitCost = row.UnitCost,
                //Quantity = row.Qty,
                //Amount = Convert.ToDecimal(row.Amount),
                //GrandTotal = Convert.ToDecimal(row.GrandTotal)


            };
            return View(model1);
        }
        [HttpPost]
        public JsonResult EditInvoice(Invoice i)
        {
            i.CreatedDate = DateTime.Now;
            db.Entry(i).State = EntityState.Modified;
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

        //[HttpPost]
        //public ActionResult DeleteInvoice(int id)
        //{
        //    if (id > 0)
        //    {
        //        try
        //        {
        //            var invoice = db.Invoices.FirstOrDefault(model => model.InvoiceID == id);
        //            if (invoice != null)
        //            {
        //                db.Invoices.Remove(invoice);
        //                int result = db.SaveChanges();

        //                if (result > 0)
        //                {
        //                    return Json(new { success = true });
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log the exception
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    return Json(new { success = false });
        //}


    }
}