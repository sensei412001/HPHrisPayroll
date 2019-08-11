using System;

namespace HPHrisPayroll.API.Dtos
{
    public class UserGroupDto
    {
        public int UserGroupId { get; set; }
        public string UserGroupName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}