using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data.Emp
{
    public interface IEmpNoConfigRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<EmployeeNoConfig>> GetEmpNoConfigs();
        Task<EmployeeNoConfig> GetEmpNoConfig(int id);
        Task<EmployeeNoConfig> GetEmpNoConfig(string companyCode);
        string GenerateEmployeeNo(string companyCode);
        Task<bool> UpdateEmployeeNoConfig(string companyCode);
        bool IsConfigExist(string companyCode);
    }
}