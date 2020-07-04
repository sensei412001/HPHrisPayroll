using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Helper;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{        
    [Authorize(Policy = "RequireCompanyRole")]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepo _repo;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(string code)
        {
            var recordFromRepo = await _repo.GetCompany(code);

            var recordToReturn = _mapper.Map<CompanyDto>(recordFromRepo);

            return Ok(recordToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetCompanies();

            var records =_mapper.Map<IEnumerable<CompanyDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                records,
                loadOptions
            );

            return Ok(recordsToReturn);
        }

        [HttpGet("LookUp")]
        public async Task<IActionResult> LookUp(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetCompanies();

            var records =_mapper.Map<IEnumerable<CompanyForLookupDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                records,
                loadOptions
            );

            return Ok(recordsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new Companies();
            JsonConvert.PopulateObject(values, obj);

            //if code is existing
            // bool brtn = true;
            // if (brtn)
            //     return BadRequest("The code already exist!");

            _repo.Add(obj);
            await _repo.SaveAll();

            var objToReturn = _mapper.Map<CompanyDto>(obj);

            return Ok(objToReturn);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRecord(string key, string values)
        {
            var obj = await _repo.GetCompany(key);
            JsonConvert.PopulateObject(values, obj);

            await _repo.SaveAll();

            var objToReturn = _mapper.Map<CompanyDto>(obj);

            return Ok(objToReturn);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string key)
        {
            var obj = await _repo.GetCompany(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }

    }
}