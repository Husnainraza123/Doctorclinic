
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace DoctorApp.Models
{

using System;
    
public partial class GetInvoiceReport_sp_Result
{

    public int InvoiceID { get; set; }

    public int Patient_id { get; set; }

    public Nullable<int> InvoiceDetailID { get; set; }

    public string Patient_Name { get; set; }

    public string Patient_Address { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Billing_Address { get; set; }

    public string Invoice_Date { get; set; }

    public string Item { get; set; }

    public string des { get; set; }

    public Nullable<decimal> price { get; set; }

    public Nullable<decimal> Qty { get; set; }

    public Nullable<decimal> Amount { get; set; }

    public Nullable<decimal> Total { get; set; }

    public Nullable<decimal> Discount { get; set; }

    public Nullable<decimal> GrandTotal { get; set; }

}

}
