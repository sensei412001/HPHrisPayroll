using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Departments = new HashSet<Departments>();
            EmployeeNoConfig = new HashSet<EmployeeNoConfig>();
            Employees = new HashSet<Employees>();
            JobLevels = new HashSet<JobLevels>();
            PayrollModes = new HashSet<PayrollModes>();
        }

        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public int CompanyUid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<EmployeeNoConfig> EmployeeNoConfig { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<JobLevels> JobLevels { get; set; }
        public virtual ICollection<PayrollModes> PayrollModes { get; set; }
    }
}
