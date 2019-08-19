using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data.Maint
{
    public interface IDeptRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Departments>> GetDepartments();
        Task<Departments> GetDepartment(string code);
    }
}