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
                    //Amount=s.Amount,
                    Total = s.Total,
                    Discount = s.Discount,
                    GrandTotal = s.GrandTotal,
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
        public JsonResult AddInvoice(Invoice i, string[] Item, string[] Description, string[] UnitCost, string[] Quantity, string[] Amount,string[] Total, string[] Discount, string[] GrandTotal)
        {
            try
            {
                // Insert the Invoice first
                i.CreatedDate = DateTime.Now;
                db.Invoices.Add(i);
                int invoiceSaveResult = db.SaveChanges();

                if (invoiceSaveResult > 0)
                {
                    // Loop through the details arrays and add them to the InvoiceDetails table
                    for (int j = 0; j < Item.Length; j++)
                    {
                        InvoiceDetail detail = new InvoiceDetail
                        {
                            InvoiceID = i.InvoiceID, // Ensure the foreign key is set

                            // Ensure the Item and Description arrays are not empty and indices are valid
                            Item = Item.Length > j ? Item[j] : string.Empty,
                            Description = Description.Length > j ? Description[j] : string.Empty,

                            // Use TryParse to safely parse the strings
                            UnitCost = decimal.TryParse(UnitCost[j], out decimal unitCostValue) ? (decimal)unitCostValue : 0,
                            Quantity = int.TryParse(Quantity[j], out int quantityValue) ? (decimal)quantityValue : 0,
                            Amount = decimal.TryParse(Amount[j], out decimal amountValue) ? (decimal?)amountValue : null,
                            Total = decimal.TryParse(Total[j], out decimal TotaltValue) ? (decimal?)TotaltValue : null,
                            Discount = decimal.TryParse(Discount[j], out decimal discountValue) ? (decimal?)discountValue : null,
                            GrandTotal = decimal.TryParse(GrandTotal[j], out decimal grandTotalValue) ? (decimal?)grandTotalValue : null,

                            CreatedBy = "Husnain Mmeon", // You can set this or modify as per your context
                            CreatedDate = DateTime.Now
                        };


                        db.InvoiceDetails.Add(detail);
                    }

                    int detailsSaveResult = db.SaveChanges();

                    if (detailsSaveResult > 0)
                    {
                        return Json(new { success = true, message = "Invoice and details added successfully." });
                    }
                }

                return Json(new { success = false, message = "Error occurred while saving the Invoice." });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
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
                Total = row.Total,
                Discount = row.Discount,
                GrandTotal = row.GrandTotal,


            };
            return View(model1);
        }
        [HttpPost]
        //public JsonResult EditInvoice(Invoice i)
        //{
        //    i.CreatedDate = DateTime.Now;
        //    db.Entry(i).State = EntityState.Modified;
        //    int a = db.SaveChanges();
        //    if (a > 0)
        //    {
        //        return Json(data: 1);
        //    }
        //    else
        //    {
        //        return Json(data: 0);

        //    }
        //}

        public JsonResult EditInvoice(Invoice i, string[] Item, string[] Description, string[] UnitCost, string[] Quantity, string[] Amount, string[] Total, string[] Discount, string[] GrandTotal)
        {
            try
            {
                // Insert the Invoice record
                i.CreatedDate = DateTime.Now;
                db.Entry(i).State = EntityState.Modified;
                int invoiceSaveResult = db.SaveChanges();

                if (invoiceSaveResult > 0)
                {
                    // Loop through details and add to InvoiceDetails table
                    for (int j = 0; j < Item.Length; j++)
                    {
                        InvoiceDetail detail = new InvoiceDetail
                        {
                            InvoiceID = i.InvoiceID,

                            Item = Item.Length > j ? Item[j] : string.Empty,
                            Description = Description.Length > j ? Description[j] : string.Empty,

                            UnitCost = decimal.TryParse(UnitCost[j], out decimal unitCostValue) ? unitCostValue : 0,
                            Quantity = int.TryParse(Quantity[j], out int quantityValue) ? quantityValue : 0,
                            Amount = decimal.TryParse(Amount[j], out decimal amountValue) ? (decimal?)amountValue : null,
                            Total = decimal.TryParse(Total[j], out decimal totalValue) ? (decimal?)totalValue : null,
                            Discount = decimal.TryParse(Discount[j], out decimal discountValue) ? (decimal?)discountValue : null,
                            GrandTotal = decimal.TryParse(GrandTotal[j], out decimal grandTotalValue) ? (decimal?)grandTotalValue : null,

                            CreatedBy = "Husnain Mmeon",
                            CreatedDate = DateTime.Now
                        };

                        db.InvoiceDetails.Add(detail);
                    }

                    int detailsSaveResult = db.SaveChanges();

                    if (detailsSaveResult > 0)
                    {
                        return Json(new { success = true, message = "Invoice and details Edit successfully." });
                    }
                }

                return Json(new { success = false, message = "Error occurred while saving the Invoice." });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }



        [HttpPost]
        public ActionResult DeleteInvoice(int id)
        {
            if (id > 0)
            {
                try
                {
                    // Find the Invoice by ID
                    var InvoiceIdRow = db.Invoices.FirstOrDefault(model => model.InvoiceID == id);

                    if (InvoiceIdRow != null)
                    {
                        // Fetch associated invoice details
                        var invoiceDetails = db.InvoiceDetails.Where(detail => detail.InvoiceID == id).ToList();

                        // Loop through and delete each InvoiceDetail
                        foreach (var detail in invoiceDetails)
                        {
                            db.Entry(detail).State = EntityState.Deleted;
                        }

                        // Delete the main Invoice
                        db.Entry(InvoiceIdRow).State = EntityState.Deleted;

                        // Save all changes to the database
                        int a = db.SaveChanges();

                        if (a > 0)
                        {
                            return Json(data: 1); // Return success
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (use proper logging mechanism in production)
                    Console.WriteLine(ex.Message);
                }
            }
            return Json(data: 0); // Return failure
        }


        //[HttpPost]
        //public JsonResult ViewInvoice(Invoice i)
        //{
        //    List<InvoiceViewModel> invoicesView = db.BrowseInvoice_sp().Select(
        //         s => new InvoiceViewModel()
        //         {
        //             InvoiceID = s.ID,
        //             PatientName = s.Patient_Name,
        //             Patient_Address = s.Patient_Address,
        //             Billing_Address = s.Billing_Address,
        //             InvoiceDate = Convert.ToDateTime(s.Invoice_Date),
        //            //Amount=s.Amount,
        //            Total = s.Total,
        //             Discount = s.Discount,
        //             GrandTotal = s.GrandTotal,
        //         }).ToList();
        //    return View(invoicesView);
        //}






    }

}