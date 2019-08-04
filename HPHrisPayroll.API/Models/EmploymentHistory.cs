using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmploymentHistory
    {
        public int EmploymentHistoryId { get; set; }
        public string EmployeeNo { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public DateTime DateStarted { get; set; }
        public DateTime? DateTo { get; set; }
        public string Position { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
