using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HPHrisPayroll.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HPHrisPayroll.API.Data.Emp
{
    public class EmpNoConfigRepo : IEmpNoConfigRepo
    {
        private readonly HpDBContext _context;
        public EmpNoConfigRepo(HpDBContext context)
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

        public async Task<IEnumerable<EmployeeNoConfig>> GetEmpNoConfigs()
        {
            var records = await _context.EmployeeNoConfig
                .ToListAsync();

            return records;
        }

        public async Task<EmployeeNoConfig> GetEmpNoConfig(int id)
        {
            var record = await _context.EmployeeNoConfig.FirstOrDefaultAsync(o => o.EmployeeNoConfigId == id);

            return record;
        }

         public async Task<EmployeeNoConfig> GetEmpNoConfig(string companyCode)
        {
            var record = await _context.EmployeeNoConfig.FirstOrDefaultAsync(o => o.CompanyCode == companyCode);

            return record;
        }

        public string GenerateEmployeeNo(string companyCode)
        {
            string srtn = string.Empty;
            var obj = _context.EmployeeNoConfig.Where(o => o.CompanyCode == companyCode).FirstOrDefault();
            if (obj != null)
            {
                long counter = obj.EmpNoCounter;
                string prefix = obj.Prefix;
                
                srtn = prefix + counter.ToString("000000");
            }            

            return srtn;
        }

        public async Task<bool> UpdateEmployeeNoConfig(string companyCode)
        {
            bool brtn = true;

            var recordFromRepo = await _context.EmployeeNoConfig
                .FirstOrDefaultAsync(o => o.CompanyCode == companyCode);

            recordFromRepo.EmpNoCounter += 1;

            await _context.SaveChangesAsync();

            return brtn;            
        }
        
        public bool IsConfigExist(string companyCode) 
        {
            bool brtn = _context.EmployeeNoConfig
                .Where(o => o.CompanyCode == companyCode)
                .Count() > 0;

            return brtn;
        }
    
    }
}