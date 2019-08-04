using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class UserGroupAccess
    {
        public int UserGroupAccessId { get; set; }
        public int UserGroupId { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Roles Role { get; set; }
        public virtual UserGroups UserGroup { get; set; }
    }
}
