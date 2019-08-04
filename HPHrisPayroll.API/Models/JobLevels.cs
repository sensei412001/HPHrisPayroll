using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class JobLevels
    {
        public JobLevels()
        {
            Positions = new HashSet<Positions>();
        }

        public string JobLevelCode { get; set; }
        public string JobLevelDescription { get; set; }
        public string CompanyCode { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
        public virtual ICollection<Positions> Positions { get; set; }
    }
}
