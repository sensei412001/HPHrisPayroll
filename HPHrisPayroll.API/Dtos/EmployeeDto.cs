using System;

namespace HPHrisPayroll.API.Dtos
{
    public class EmployeeDto
    {
        public string EmployeeNo { get; set; }
        public string CompanyCode { get; set; }
        public string DeptCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCountry { get; set; }
        public string CurrentAddress { get; set; }
        public string EmployeeStatusCode { get; set; }
        public string Position { get; set; }
        public string PayrollMode { get; set; }
        public decimal Salary { get; set; }
        public string Tinno { get; set; }
        public string TaxStatus { get; set; }
        public string Sssno { get; set; }
        public string Hdmfno { get; set; }
        public string PhilHealthNo { get; set; }
        public DateTime? StartDate { get; set; }
        public long EmpUid { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

    }
}