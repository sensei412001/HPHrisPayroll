using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmailAddresses
    {
        public int EmailAddId { get; set; }
        public string EmployeeNo { get; set; }
        public string EmailAddress { get; set; }
        public string TypeCode { get; set; }
        public bool IsPreffered { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
        public virtual EmailTypes TypeCodeNavigation { get; set; }
    }
}
