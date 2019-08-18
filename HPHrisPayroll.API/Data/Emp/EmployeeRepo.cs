using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data.Emp
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly HpDBContext _context;
        public EmployeeRepo(HpDBContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            var records = await _context.Employees
                .ToListAsync();

            return records;
        }

        public async Task<IEnumerable<Employees>> GetActiveEmployees()
        {
            var records = await _context.Employees
                .Where(o => o.EmployeeStatusCode != "Resigned")
                .ToListAsync();

            return records;
        }

        public async Task<Employees> GetEmployee(string employeeNo)
        {
            var record = await _context.Employees
                .FirstOrDefaultAsync(o => o.EmployeeNo == employeeNo);

            return record;
        }

        public bool IsEmployeeNoExist(string employeeNo) 
        {
            bool brtn = _context.Employees.Where(o => o.EmployeeNo == employeeNo).Count() > 0;

            return brtn;
        }

        public string GenerateEmployeeNo(string companyCode)
        {
            string srtn = string.Empty;
            var obj = _context.EmployeeNoConfig.Where(o => o.CompanyCode == companyCode).FirstOrDefault();
            if (obj != null)
            {
                long counter = obj.EmpNoCounter;
                string prefix = obj.Prefix;
                
                srtn = prefix + counter.ToString();
            }            

            return srtn;
        }

        public async Task<bool> UpdateEmployeeNoConfig(string companyCode)
        {
            bool brtn = true;

            var recordFromRepo = await _context.EmployeeNoConfig
                .FirstOrDefaultAsync(o => o.CompanyCode == companyCode);

            recordFromRepo.EmpNoCounter += 1;

            await _context.SaveChangesAsync();

            return brtn;            
        }

        public async Task<bool> IsEmployeeExist(
            string lastName, string firstName, string middleName, string gender, DateTime BirthDate)
        {            
            var obj = await _context.Employees.Where(o => o.LastName == lastName &&
                o.FirstName == firstName && 
                o.MiddleName == o.MiddleName && 
                o.Gender == gender && 
                o.BirthDate.ToString("MM/dd/yyyy") == BirthDate.ToString("MM/dd/yyyy"))
            .FirstOrDefaultAsync();

            bool brtn = obj != null;

            return brtn;
        }
       
    }
}