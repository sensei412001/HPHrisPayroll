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

        [HttpGet("UserGroupAccess/{userGroupId}")]
        public async Task<IActionResult> UserGroupAccess(int userGroupId, DataSourceLoadOptions loadOptions)
        {            
            // VALIDATIONS
            if (userGroupId == 0)
                return BadRequest("Invalid user group!");

            var groupAccess = await _repo.GetUserGroupAccess(userGroupId);                        
            var roles = await _repo.GetRoles();

            var userGroupAccess = (
                from x in roles join y in groupAccess on x.RoleId equals y.RoleId into jointable
                from z in jointable.DefaultIfEmpty()
                select new  UserGroupAccessDto {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName,
                    HasAccess = z != null,
                    UserGroupId = userGroupId,
                    UserGroupAccessId = z != null ? z.UserGroupAccessId : 0,
                    CreatedBy = z != null ? z.CreatedBy : string.Empty,
                    DateCreated = z != null ? z.DateCreated as DateTime? : null,
                }

            );

            var recordsToReturn = DataSourceLoader.Load(
                userGroupAccess,
                loadOptions
            );


            return Ok(recordsToReturn);
        }
       
        [HttpPost("AddAccess")]
        public async Task<IActionResult> AddAccess(string values)
        {
            var obj = new UserGroupAccess();
            JsonConvert.PopulateObject(values, obj);

            // VALIDATE IF EXISTING
            if (_repo.IsUserGroupAccessExist(obj.UserGroupId, obj.RoleId))
                return BadRequest("The role already exist in this User Group.");

            obj.CreatedBy = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _repo.Add(obj);
            await _repo.SaveAll();

            return Ok();
        }

        [HttpDelete("RemoveAccess")]
        public async Task<IActionResult> RemoveAccess(int key)
        {
            var obj = await _repo.GetUserGroupAccessById(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }

    }
}