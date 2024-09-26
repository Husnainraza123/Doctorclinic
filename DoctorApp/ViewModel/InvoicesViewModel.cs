using DoctorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.ViewModel
{
    public class InvoicesViewModel
    {
        public List<BrowseInvoiceByID_sp_Result> ListInvoices { set; get; }
        public BrowseInvoiceByID_sp_Result Invoice { set; get; }

        public List<GetInvoiceReport_sp_Result> ListInvoicesView { set; get; }
    }
}