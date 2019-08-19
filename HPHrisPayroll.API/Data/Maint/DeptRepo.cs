using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data.Maint
{
    public class DeptRepo : IDeptRepo
    {
        private readonly HpDBContext _context;
        public DeptRepo(HpDBContext context)
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

        public async Task<IEnumerable<Departments>> GetDepartments()
        {
            var records = await _context.Departments
                .Include(o => o.CompanyCodeNavigation)
                .ToListAsync();

            return records;
        }

        public async Task<Departments> GetDepartment(string code)
        {
            var record = await _context.Departments
                .Include(o => o.CompanyCodeNavigation)
                .FirstOrDefaultAsync(o => o.DeptCode == code);

            return record;
        }
        
    }
}