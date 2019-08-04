using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeStatus
    {
        public EmployeeStatus()
        {
            Employees = new HashSet<Employees>();
        }

        public string EmployeeStatusCode { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
