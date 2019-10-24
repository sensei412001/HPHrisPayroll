using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeNoConfig
    {
        public int EmployeeNoConfigId { get; set; }
        public string Prefix { get; set; }
        public long EmpNoCounter { get; set; }
        public string CompanyCode { get; set; }
        public string Year { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateLastUpdated { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
    }
}
