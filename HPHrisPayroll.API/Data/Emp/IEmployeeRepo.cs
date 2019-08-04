using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data.Emp
{
    public interface IEmployeeRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Employees>> GetEmployees();
        Task<Employees> GetEmployee(string employeeNo);
        bool IsEmployeeNoExist(string employeeNo);
        
        Task<bool> IsEmployeeExist(string lastName, string firstName, string middleName, string gender, DateTime BirthDate);

    }
}