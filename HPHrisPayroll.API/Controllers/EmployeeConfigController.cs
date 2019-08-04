using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data.Emp;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeConfigController : ControllerBase
    {
        private readonly IEmpNoConfigRepo _repo;
        public EmployeeConfigController(IEmpNoConfigRepo repo)
        {
            _repo = repo;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpNoConfig(int id)
        {
            var recordFromRepo = await _repo.GetEmpNoConfig(id);

            return Ok(recordFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpNoConfigs(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetEmpNoConfigs();

            var companiesToReturn = DataSourceLoader.Load(
                recordsFromRepo,
                loadOptions
            );

            return Ok(companiesToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new EmployeeNoConfig();
            JsonConvert.PopulateObject(values, obj);

            string companyCode = obj.CompanyCode;

            // VALIDATE IF EXISTING
            bool isExist = _repo.IsConfigExist(companyCode);
            if (isExist)
                return BadRequest("The config for the company \"" + companyCode + "\" already exist!");
            
            if (obj.EmpNoCounter < 1)
                return BadRequest("The counter must be greater than zero (0)!");

            _repo.Add(obj);
            await _repo.SaveAll();

            return Ok(obj);
        }        

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(int key)
        {
            var obj = await _repo.GetEmpNoConfig(key);
            if (obj.EmpNoCounter > 0)
                return BadRequest("This can't be deleted as it has been used to generate employee number!");

            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }


    }
}