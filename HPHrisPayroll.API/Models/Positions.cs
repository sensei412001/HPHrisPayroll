using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Positions
    {
        public Positions()
        {
            Employees = new HashSet<Employees>();
        }

        public string Position { get; set; }
        public string Description { get; set; }
        public string JobLevelCode { get; set; }
        public string CompanyCode { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual JobLevels JobLevelCodeNavigation { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
