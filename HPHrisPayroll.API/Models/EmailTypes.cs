using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmailTypes
    {
        public EmailTypes()
        {
            EmailAddresses = new HashSet<EmailAddresses>();
        }

        public string Code { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<EmailAddresses> EmailAddresses { get; set; }
    }
}
