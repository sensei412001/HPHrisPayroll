using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class EmployeeFiles
    {
        public long EmployeeFileId { get; set; }
        public string EmployeeNo { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
