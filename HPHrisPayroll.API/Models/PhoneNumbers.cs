using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class PhoneNumbers
    {
        public int PhoneId { get; set; }
        public string EmployeeNo { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsPreffered { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
