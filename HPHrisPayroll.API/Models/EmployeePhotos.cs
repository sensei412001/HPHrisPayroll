using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeePhotos
    {
        public int PhotoId { get; set; }
        public string EmployeeNo { get; set; }
        public string Location { get; set; }
        public string Extension { get; set; }
        public bool IsPrimary { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
    }
}
