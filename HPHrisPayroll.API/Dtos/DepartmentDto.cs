using System;

namespace HPHrisPayroll.API.Dtos
{
    public class DepartmentDto
    {
        public string DeptCode { get; set; }
        public string Description { get; set; }
        public string CompanyCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}