using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{    
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
            var companyFromRepo = await _repo.GetCompany(code);

            var companyToReturn = _mapper.Map<CompanyDto>(companyFromRepo);

            return Ok(companyToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies(DataSourceLoadOptions loadOptions)
        {
            var companiesFromRepo = await _repo.GetCompanies();

            var companies =_mapper.Map<IEnumerable<CompanyDto>>(companiesFromRepo);

            var companiesToReturn = DataSourceLoader.Load(
                companies,
                loadOptions
            );

            return Ok(companiesToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new Companies();
            JsonConvert.PopulateObject(values, obj);

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