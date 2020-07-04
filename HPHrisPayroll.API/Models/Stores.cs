using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Stores
    {
        public Stores()
        {
            Employees = new HashSet<Employees>();
        }

        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string CompanyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
