using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using HPHrisPayroll.API.Data;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Helper;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HPHrisPayroll.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repo;
        private readonly IMapper _mapper;
        public UsersController(IUsersRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string username)
        {
            var userFromRepo = await _repo.GetUser(username);

            var userToReturn = _mapper.Map<UserDto>(userFromRepo);

            return Ok(userToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(DataSourceLoadOptions loadOptions)
        {
            var usersFromRepo = await _repo.GetUsers();

            var users =_mapper.Map<IEnumerable<UserDto>>(usersFromRepo);

            var usersToReturn = DataSourceLoader.Load(
                users,
                loadOptions
            );

            return Ok(usersFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(string values)
        {
            var obj = new Users();
            JsonConvert.PopulateObject(values, obj);
                    
            string randomPassword = obj.Syek;
            byte[] passwordHash, passwordSalt;

            Utility.CreatePasswordHash(randomPassword, out passwordHash, out passwordSalt);            
            obj.PasswordHash = passwordHash;
            obj.PasswordSalt = passwordSalt;
            obj.PasswordExpiration = DateTime.Now.AddMonths(3);

            _repo.Add(obj);
            await _repo.SaveAll();

            // SEND THE USER CRED TO USER
            if (obj.IsEnable)
            {
                var user = await _repo.GetUser(obj.UserName);

                string toEmail = user.EmployeeNoNavigation.EmailAddresses
                    .Where(o => o.IsPreffered == true)
                    .FirstOrDefault().EmailAddress;

                string subject = string.Empty;
                string body = string.Empty;

                await Utility.SendEmail(toEmail, subject, body, string.Empty);
            }

            var objToReturn = _mapper.Map<UserDto>(obj);

            return Ok(objToReturn);
        }        

        [HttpPut]
        public async Task<IActionResult> UpdateRecord(string key, string values)
        {
            var obj = await _repo.GetUser(key);
            JsonConvert.PopulateObject(values, obj);

            await _repo.SaveAll();

            var objToReturn = _mapper.Map<UserDto>(obj);

            return Ok(objToReturn);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecord(string key)
        {
            var obj = await _repo.GetUser(key);
            _repo.Delete(obj);
            await _repo.SaveAll();

            return Ok();
        }

    }
}