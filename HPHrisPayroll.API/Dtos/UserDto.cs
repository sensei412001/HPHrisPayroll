using System;

namespace HPHrisPayroll.API.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? UserGroupId { get; set; }
        public string EmployeeNo { get; set; }
        public bool IsEnable { get; set; }
        public long UserUid { get; set; }
        public DateTime? LastActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}