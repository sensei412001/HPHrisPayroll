using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Users
    {
        public Users()
        {
            UserCompanies = new HashSet<UserCompanies>();
        }

        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int? UserGroupId { get; set; }
        public string EmployeeNo { get; set; }
        public bool IsEnable { get; set; }
        public long UserUid { get; set; }
        public DateTime? LastActive { get; set; }
        public string Syek { get; set; }
        public DateTime? PasswordExpiration { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Employees EmployeeNoNavigation { get; set; }
        public virtual UserGroups UserGroup { get; set; }
        public virtual ICollection<UserCompanies> UserCompanies { get; set; }
    }
}
