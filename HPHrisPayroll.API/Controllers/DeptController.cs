using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data.Maint;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Helper;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{
    [Authorize(Policy = "RequireDeptRole")]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    public class DeptController : ControllerBase
    {
        private readonly IDeptRepo _repo;
        private readonly IMapper _mapper;
        public DeptController(IDeptRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(string code)
        {
            var recordFromRepo = await _repo.GetDepartment(code);

            var recordToReturn = _mapper.Map<DepartmentDto>(recordFromRepo);

            return Ok(recordToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetDepartments();

            var records =_mapper.Map<IEnumerable<DepartmentDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                records,
                loadOptions
            );

            return Ok(recordsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new Departments();
            JsonConvert.PopulateObject(values, obj);

            _repo.Add(obj);
            await _repo.SaveAll();

            var objToReturn = _mapper.Map<DepartmentDto>(obj);

            return Ok(objToReturn);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRecord(string key, string values)
        {
            var obj = await _repo.GetDepartment(key);
            JsonConvert.PopulateObject(values, obj);

            await _repo.SaveAll();

            var objToReturn = _mapper.Map<DepartmentDto>(obj);

            return Ok(objToReturn);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string key)
        {
            var obj = await _repo.GetDepartment(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }

    }
}