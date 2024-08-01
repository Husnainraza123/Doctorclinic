using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class ScheduleViewModel:Schedule
    {
        public string DoctorName { get; set; }
        public string DepartmentsName { get; set; }
        public string ImageFile { get; internal set; }
     
    }
}