using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class DoctorProfileViewModal 
    {
        public List<EducationInformation> EducationList { get; set; }
        public List<ExperienceInformation> ExperienceList { get; set; }
        public Doctor Doctor { get; set; }

    }
}