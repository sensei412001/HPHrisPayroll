using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeePayouts
    {
        public long PayoutId { get; set; }
        public string BatchNo { get; set; }
        public string CompanyCode { get; set; }
        public string DeptCode { get; set; }
        public string EmployeeNo { get; set; }
        public decimal Amount { get; set; }
        public string PayrollMode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
