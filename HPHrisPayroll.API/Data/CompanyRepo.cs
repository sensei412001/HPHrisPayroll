using System.Collections.Generic;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly HpDBContext _context;
        public CompanyRepo(HpDBContext context)
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

        public async Task<IEnumerable<Companies>> GetCompanies()
        {
            var companies = await _context.Companies
                .ToListAsync();

            return companies;
        }

        public async Task<Companies> GetCompany(string code)
        {
            var comp = await _context.Companies.FirstOrDefaultAsync(o => o.CompanyCode == code);

            return comp;
        }


    }
}