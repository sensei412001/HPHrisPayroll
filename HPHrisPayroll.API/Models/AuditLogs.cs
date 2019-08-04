using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class AuditLogs
    {
        public long LogId { get; set; }
        public int UserId { get; set; }
        public string Ipaddress { get; set; }
        public string ControllerName { get; set; }
        public string Method { get; set; }
        public string Activty { get; set; }
        public DateTime DateAccessed { get; set; }
    }
}
