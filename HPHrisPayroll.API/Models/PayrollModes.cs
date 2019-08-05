using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class PayrollModes
    {
        public PayrollModes()
        {
            Employees = new HashSet<Employees>();
        }

        public string PayrollMode { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
