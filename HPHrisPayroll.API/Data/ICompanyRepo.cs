using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data
{
    public interface ICompanyRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Companies>> GetCompanies();
        Task<Companies> GetCompany(string code);
    }
}