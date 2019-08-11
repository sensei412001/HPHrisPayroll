using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;

namespace HPHrisPayroll.API.Data
{
    public interface IUserGroupAccessRepo
    {
        void Add<T>(T entity) where T: class;        
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAll();
        Task<IEnumerable<UserGroupAccess>> GetUserGroupAccess(int userGroupId);
        Task<UserGroupAccess> GetUserGroupAccessById(int id);        
        Task<IEnumerable<Roles>> GetRoles();

        bool IsUserGroupAccessExist(int userGroupId, int roleId);
        
    }
}