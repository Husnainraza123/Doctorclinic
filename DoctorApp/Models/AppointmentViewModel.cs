using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class AppointmentViewModel : Appointment
    {
        public string PatientName { get; set; }
        public int? Age { get; set; }
        public string DoctorName { get; set; }
        public DateTime? DOB { get; set; }
        public string AppointmentTime { get; set; }
        public string EndTime { get; set; }
        public string DepartmentsName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }   
        public string Description { get; set; }
        public string ImageFile { get; internal set; }
        public bool Status { get; set; }
        

        
    }
}