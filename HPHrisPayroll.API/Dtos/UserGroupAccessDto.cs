using System;

namespace HPHrisPayroll.API.Dtos
{
    public class UserGroupAccessDto
    {
        public int UserGroupAccessId { get; set; }
        public int UserGroupId { get; set; }        
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasAccess { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}