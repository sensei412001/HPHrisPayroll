using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmergencyContacts
    {
        public long EmergencyContactId { get; set; }
        public string EmployeeNo { get; set; }
        public string ContactName { get; set; }
        public string RelationshipToEmployee { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateTimeCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
