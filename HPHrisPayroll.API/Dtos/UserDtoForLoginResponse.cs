namespace HPHrisPayroll.API.Dtos
{
    public class UserDtoForLoginResponse
    {
        public string UserName { get; set; }        
        public int? UserGroupId { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public bool IsEnable { get; set; }
        public long UserUid { get; set; }
                
    }
}