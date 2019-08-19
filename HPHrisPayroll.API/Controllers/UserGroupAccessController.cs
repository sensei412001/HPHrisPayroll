using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Authorize(Policy = "RequireUserGroupAccessRole")]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    public class UserGroupAccessController: ControllerBase
    {
        private readonly IUserGroupAccessRepo _repo;
        private readonly IMapper _mapper;

        public UserGroupAccessController(IUserGroupAccessRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupAccessById(int id)
        {
            var recordFromRepo = await _repo.GetUserGroupAccessById(id);

            var recordToReturn = _mapper.Map<UserGroupAccessDto>(recordFromRepo);

            return Ok(recordToReturn);
        }

        [HttpGet("Access/{userGroupId}")]
        public async Task<IActionResult> Access(int userGroupId, DataSourceLoadOptions loadOptions)
        {            
            // VALIDATIONS
            if (userGroupId == 0)
                return BadRequest("Invalid user group!");

            var recordsFromRepo = await _repo.GetUserGroupAccess(userGroupId);            
            
            var mappedRecords = _mapper.Map<IEnumerable<UserGroupAccessDto>>(recordsFromRepo);

            var recordsToReturn = DataSourceLoader.Load(
                mappedRecords,
                loadOptions
            );


            return Ok(recordsToReturn);
        }
       
        [HttpPut]
        public async Task<IActionResult> UpdateRecord(int key, string values)
        {
            var obj = await _repo.GetUserGroupAccessById(key);
            JsonConvert.PopulateObject(values, obj);

            // check for new roles and add to selected user group
            int userGroupId = obj.UserGroupId;
            string createdBy = obj.UserGroup.CreatedBy;

            var roles = await _repo.GetRoles();
            var access = await _repo.GetUserGroupAccess(userGroupId);

            if (roles.Count() != access.Count())
            {
                var rolesToAdd = roles.Where(o => !access.Select(b => b.RoleId).Contains(o.RoleId));
                foreach(var role in rolesToAdd)
                {
                    UserGroupAccess rowObj = new UserGroupAccess();
                    rowObj.UserGroupId = userGroupId;
                    rowObj.RoleId = role.RoleId;
                    rowObj.CreatedBy = createdBy;
                    
                    _repo.Add(rowObj);
                }
            }                                   
            
            // save has access update and new roles to add in the user group access
            await _repo.SaveAll();

            var objToReturn = _mapper.Map<UserGroupAccessDto>(obj);

            return Ok(objToReturn);
        }

        private async void AddNewRolesToUserGroupAccess(int userGroupId, string createdBy)
        {
            var roles = await _repo.GetRoles();
            var access = await _repo.GetUserGroupAccess(userGroupId);

            if (roles.Count() == access.Count())
                return;

            // get roles that don't exist yet in table group access
            var rolesToAdd = roles.Where(o => !access.Select(b => b.RoleId).Contains(o.RoleId));

            foreach(var role in rolesToAdd)
            {
                UserGroupAccess rowObj = new UserGroupAccess();
                rowObj.UserGroupId = userGroupId;
                rowObj.RoleId = role.RoleId;
                rowObj.CreatedBy = createdBy;
                
                _repo.Add(rowObj);
                await _repo.SaveAll();
            }
        }
        
    }
}