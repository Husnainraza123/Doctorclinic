
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
    
public partial class Doctor
{

    public int DoctorID { get; set; }

    public Nullable<int> EducationID { get; set; }

    public Nullable<int> ExperienceID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public System.DateTime DOB { get; set; }

    public string Gender { get; set; }

    public string Address { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string Phone { get; set; }

    public string PostalCode { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string CreatedBy { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public string ModifyBy { get; set; }

    public Nullable<System.DateTime> ModifyDate { get; set; }

    public string Status { get; set; }
        public string ImageFile { get; internal set; }
    }

}
