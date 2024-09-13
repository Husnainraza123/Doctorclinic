using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class InvoiceViewModel:Invoice
    {
        public string PatientName { get; set; }
        public int InvoiceID { get; set; }

        public int PatientsID { get; set; }


        public string Patient_Address { get; set; }

        public string Billing_Address { get; set; }

        public System.DateTime InvoiceDate { get; set; }

        public string Item { get; set; }

        public string Description { get; set; }

        public decimal UnitCost { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }

        public decimal Discount { get; set; }



        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }

        public string ModifyBy { get; set; }

        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}