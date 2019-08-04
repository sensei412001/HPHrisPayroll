using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class TaxStatus
    {
        public TaxStatus()
        {
            Employees = new HashSet<Employees>();
        }

        public string TaxStatusCode { get; set; }
        public string TaxStatusDesc { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
