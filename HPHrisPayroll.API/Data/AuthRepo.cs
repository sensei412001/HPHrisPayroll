using System.Linq;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly HpDBContext _context;        

        public AuthRepo(HpDBContext context)
        {
            _context = context;
        }

        public async Task<Users> Login(string username, string password)
        {
            var user  = await _context.Users
                .Include(o => o.EmployeeNoNavigation).ThenInclude(email => email.EmailAddresses)
                .Include(o => o.UserGroup.UserGroupAccess).ThenInclude(uga => uga.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
                return null; 

            if (password != user.Syek)
                return null;
            
            // if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //     return null;

            return user;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<Users> GetUser(string username)
        {
            var recordFromRepo = await _context.Users
                .Include(o => o.EmployeeNoNavigation).ThenInclude(email => email.EmailAddresses)
                .Include(o => o.UserGroup.UserGroupAccess).ThenInclude(uga => uga.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);            
            return recordFromRepo;            
        }

    }
}