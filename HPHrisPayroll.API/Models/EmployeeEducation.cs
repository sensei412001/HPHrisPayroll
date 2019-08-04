using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeEducation
    {
        public int EducationRefId { get; set; }
        public string EmployeeNo { get; set; }
        public string School { get; set; }
        public string Course { get; set; }
        public DateTime? DateGraduated { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
