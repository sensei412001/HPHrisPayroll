using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Roles
    {
        public Roles()
        {
            UserGroupAccess = new HashSet<UserGroupAccess>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserGroupAccess> UserGroupAccess { get; set; }
    }
}
