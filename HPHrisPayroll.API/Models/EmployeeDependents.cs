using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeDependents
    {
        public int DependentId { get; set; }
        public string EmployeeNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Relationship { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
