using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data
{
    public class UsersRepo : IUsersRepo
    {
        private readonly HpDBContext _context;
        public UsersRepo(HpDBContext context)
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

        public async Task<IEnumerable<Users>> GetUsers()
        {
            var users = await _context.Users
                .Include(o => o.EmployeeNoNavigation).ThenInclude(sub => sub.EmployeeAddresses)
                .Include(o => o.UserGroup)
                .ToListAsync();

            return users;
        }

        public async Task<Users> GetUser(string username)
        {
            var user = await _context.Users
                .Include(o => o.EmployeeNoNavigation).ThenInclude(sub => sub.EmployeeAddresses)
                .FirstOrDefaultAsync(o => o.UserName == username);

            return user;
        }

        public async Task<Users> GetUser(int uid)
        {
            var user = await _context.Users
                .Include(o => o.EmployeeNoNavigation).ThenInclude(sub => sub.EmployeeAddresses)
                .FirstOrDefaultAsync(o => o.UserUid == uid);

            return user;
        }



    }
}