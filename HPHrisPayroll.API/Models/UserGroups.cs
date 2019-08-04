using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class UserGroups
    {
        public UserGroups()
        {
            UserGroupAccess = new HashSet<UserGroupAccess>();
            Users = new HashSet<Users>();
        }

        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<UserGroupAccess> UserGroupAccess { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
