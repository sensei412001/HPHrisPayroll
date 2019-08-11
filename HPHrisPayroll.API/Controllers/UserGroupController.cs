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
    [Authorize(Policy = "RequireUserGroupsRole")]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    public class UserGroupController: ControllerBase
    {
        private readonly IUserGroupRepo _repo;
        private readonly IMapper _mapper;

        public UserGroupController(IUserGroupRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup(int id)
        {
            var recordFromRepo = await _repo.GetUserGroup(id);

            var recordToReturn = _mapper.Map<UserGroupDto>(recordFromRepo);

            return Ok(recordToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGroups(DataSourceLoadOptions loadOptions)
        {
            var recordsFromRepo = await _repo.GetUserGroups();

            var records =_mapper.Map<IEnumerable<UserGroupDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                records,
                loadOptions
            );

            return Ok(recordsToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new UserGroups();
            JsonConvert.PopulateObject(values, obj);

            _repo.Add(obj);
            await _repo.SaveAll();

            var objToReturn = _mapper.Map<UserGroupDto>(obj);

            return Ok(objToReturn);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecord(int key, string values)
        {
            var obj = await _repo.GetUserGroup(key);
            JsonConvert.PopulateObject(values, obj);

            await _repo.SaveAll();

            var objToReturn = _mapper.Map<UserGroupDto>(obj);

            return Ok(objToReturn);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(int key)
        {
            var obj = await _repo.GetUserGroup(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }
        

    }
}