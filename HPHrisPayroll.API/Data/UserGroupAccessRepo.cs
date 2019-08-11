using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data
{
    public class UserGroupAccessRepo: IUserGroupAccessRepo
    {
        private readonly HpDBContext _context;

        public UserGroupAccessRepo(HpDBContext context)
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

        public async Task<IEnumerable<UserGroupAccess>> GetUserGroupAccess(int userGroupId)
        {                       
            var recordsFromRepo = await _context.UserGroupAccess
                .Where(o => o.UserGroupId == userGroupId)
                .ToListAsync();
                   
            return recordsFromRepo;
        }

        public async Task<UserGroupAccess> GetUserGroupAccessById(int id)
        {
            var record = await _context.UserGroupAccess.FirstOrDefaultAsync(o => o.UserGroupAccessId == id);

            return record;
        }

        public async Task<IEnumerable<Roles>> GetRoles()
        {
            var records = await _context.Roles.ToListAsync();
            return records;
        }

        public bool IsUserGroupAccessExist(int userGroupId, int roleId)
        {
            bool isExist = _context.UserGroupAccess
                .Where(o => o.UserGroupId == userGroupId && o.RoleId == roleId).Count() > 0;

            return isExist;
        }
       
    }
}