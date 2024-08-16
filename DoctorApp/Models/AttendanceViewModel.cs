using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class AttendanceViewModel:Attendance
    {
        public string EmployeeName { get; set; }
    }
}