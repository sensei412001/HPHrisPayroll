using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Banks
    {
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
