using System;
using System.Collections.Generic;

namespace HPHrisPayroll.API.Models
{
    public partial class Employees
    {
        public Employees()
        {
            EmailAddresses = new HashSet<EmailAddresses>();
            EmergencyContacts = new HashSet<EmergencyContacts>();
            EmployeeAddresses = new HashSet<EmployeeAddresses>();
            EmployeeDependents = new HashSet<EmployeeDependents>();
            EmployeeEducation = new HashSet<EmployeeEducation>();
            EmployeePhotos = new HashSet<EmployeePhotos>();
            EmploymentHistory = new HashSet<EmploymentHistory>();
            PhoneNumbers = new HashSet<PhoneNumbers>();
            Users = new HashSet<Users>();
        }

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
        public string TaxStatusCode { get; set; }
        public string Sssno { get; set; }
        public string Hdmfno { get; set; }
        public string PhilHealthNo { get; set; }
        public DateTime? StartDate { get; set; }
        public long EmpUid { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateTimeUpdated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Companies CompanyCodeNavigation { get; set; }
        public virtual Departments DeptCodeNavigation { get; set; }
        public virtual EmployeeStatus EmployeeStatusCodeNavigation { get; set; }
        public virtual PayrollModes PayrollModeNavigation { get; set; }
        public virtual Positions PositionNavigation { get; set; }
        public virtual TaxStatus TaxStatusCodeNavigation { get; set; }
        public virtual ICollection<EmailAddresses> EmailAddresses { get; set; }
        public virtual ICollection<EmergencyContacts> EmergencyContacts { get; set; }
        public virtual ICollection<EmployeeAddresses> EmployeeAddresses { get; set; }
        public virtual ICollection<EmployeeDependents> EmployeeDependents { get; set; }
        public virtual ICollection<EmployeeEducation> EmployeeEducation { get; set; }
        public virtual ICollection<EmployeePhotos> EmployeePhotos { get; set; }
        public virtual ICollection<EmploymentHistory> EmploymentHistory { get; set; }
        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
