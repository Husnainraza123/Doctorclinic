
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
    using System.Collections.Generic;
    
public partial class InvoiceDetail:Invoice
{

    public int InvoiceDetailID { get; set; }

    public int InvoiceID { get; set; }

    public string Item { get; set; }

    public string Description { get; set; }

    public decimal UnitCost { get; set; }

    public decimal Quantity { get; set; }

    public decimal Amount { get; set; }

    public decimal Discount { get; set; }

    public decimal GrandTotal { get; set; }

    public string CreatedBy { get; set; }

    public System.DateTime CreatedDate { get; set; }

    public string ModifyBy { get; set; }

    public Nullable<System.DateTime> ModifyDate { get; set; }

}

}
