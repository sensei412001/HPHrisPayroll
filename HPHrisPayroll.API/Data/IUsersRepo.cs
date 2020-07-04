using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data
{
    public interface IUsersRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<Users>> GetUsers();
        Task<Users> GetUser(string username);
        Task<Users> GetUser(int uid);

    }
}