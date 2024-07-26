using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class EmployeeViewModel:Employee
    {
        public string RoleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public System.DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string Status { get; set; }
    }
}