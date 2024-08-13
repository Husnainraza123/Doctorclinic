using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class LeaveViewModel:Leave_
    {
        public string EmployeeName { get; set; }
        public int? Days { get; set; }
    }
}