using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data.Emp;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Helper;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{
    [Authorize(Policy = "RequireEmployeeRole")]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;
        private readonly IMapper _mapper;
        private readonly IEmpNoConfigRepo _empConfigRepo;
        public EmployeeController(IEmployeeRepo repo, 
            IEmpNoConfigRepo empConfigRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _empConfigRepo = empConfigRepo;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string employeeNo)
        {
            var recordFromRepo = await _repo.GetEmployee(employeeNo);

            var recordToReturn = _mapper.Map<EmployeeDto>(recordFromRepo);

            return Ok(recordToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetEmployees();

            var records =_mapper.Map<IEnumerable<EmployeeDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                records,
                loadOptions
            );

            return Ok(recordsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new Employees();
            JsonConvert.PopulateObject(values, obj);

            string companyCode = obj.CompanyCode;

            // VALIDATION
            bool isEmployeeExist = await _repo.IsEmployeeExist(obj.LastName, obj.FirstName, obj.MiddleName, obj.Gender, obj.BirthDate);
            if (isEmployeeExist)
                return BadRequest("The employee already exist! Please double check all the information.");

            var config = await _empConfigRepo.GetEmpNoConfig(companyCode);
            if (config == null)
                return BadRequest("Please setup the employee number counter and prefix in the cofig page.");            

            // GENERATE EMPLOYEE NUMBER
            obj.EmployeeNo = _empConfigRepo.GenerateEmployeeNo(companyCode);
            await _empConfigRepo.UpdateEmployeeNoConfig(companyCode); // INCREMENT COUNTER +=1            
                

            _repo.Add(obj);
            await _repo.SaveAll();

            var recordToReturn = _mapper.Map<EmployeeDto>(obj);

            return Ok(recordToReturn);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRecord(string key, string values)
        {
            var obj = await _repo.GetEmployee(key);
            if (obj == null)
                return BadRequest("The employee does not exist!");

            JsonConvert.PopulateObject(values, obj);

            obj.DateTimeUpdated = DateTime.Today;

            await _repo.SaveAll();

            var recordToReturn = _mapper.Map<EmployeeDto>(obj);

            return Ok(recordToReturn);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string key)
        {
            var obj = await _repo.GetEmployee(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }

        


    }
}