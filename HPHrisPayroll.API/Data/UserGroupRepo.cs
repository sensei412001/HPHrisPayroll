using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data
{
    public class UserGroupRepo: IUserGroupRepo
    {
        private readonly HpDBContext _context;

        public UserGroupRepo(HpDBContext context)
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

        public async Task<IEnumerable<UserGroups>> GetUserGroups()
        {
            var recordsFromRepo = await _context.UserGroups
                .ToListAsync();

            return recordsFromRepo;
        }

        public async Task<UserGroups> GetUserGroup(int id)
        {
            var recordFromRepo = await _context.UserGroups.FirstOrDefaultAsync(o => o.UserGroupId == id);

            return recordFromRepo;
        }


    }
}