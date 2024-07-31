using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorApp.Models
{
    public class EductionViewModel
    {
        public int EducationID { get; set; }
        public int DoctorID { get; set; }
        public string InstitutionName { get; set; }
        public string Degree { get; set; }
        public System.DateTime StartingDate { get; set; }
        public System.DateTime CompleteDate { get; set; }
        public int ExperienceID { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public string ExperiencePeriod { get; set; }
        public System.DateTime PeriodFrom { get; set; }
        public System.DateTime PeriodTo { get; set; }
    }
}