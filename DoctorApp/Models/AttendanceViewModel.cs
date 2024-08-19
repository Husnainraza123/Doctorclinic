using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class AttendanceViewModel:Attendance
    {
        
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }      
        public Dictionary<int, string> DayStatus { get; set; } // Key: Day, Value: Status (Present/Absent)
    }
}