using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class UserCompanies
    {
        public int UserCompanyId { get; set; }
        public string UserName { get; set; }
        public string CompanyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
        public virtual Users UserNameNavigation { get; set; }
    }
}
