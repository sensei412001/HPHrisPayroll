using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeAddresses
    {
        public int EmployeeAddressId { get; set; }
        public string EmployeeNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
