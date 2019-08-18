using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HPHrisPayroll.API.Data;
using HPHrisPayroll.API.Dtos;
using HPHrisPayroll.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HPHrisPayroll.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _authRepo;
        private readonly IUsersRepo _usersRepo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepo authRepo, IUsersRepo usersRepo, IConfiguration config, IMapper mapper)
        {            
            _authRepo = authRepo;
            _usersRepo = usersRepo;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            string username = loginDto.Username;
            string password = loginDto.Password;

            var userFromRepo = await _authRepo.Login(username, password);

            // VALIDATIONS
            if (userFromRepo == null)
                return BadRequest("Invalid user.");

            if (!userFromRepo.IsEnable)
                return BadRequest("Your account is disabled! Please contact HR thru their helpdesk system!");

            if (userFromRepo.PasswordExpiration <= DateTime.Today)
                return BadRequest("You password is expired!");

            string employeeNo = userFromRepo.EmployeeNo;
            string givenName = 
                userFromRepo.EmployeeNoNavigation.FirstName + ' ' +
                userFromRepo.EmployeeNoNavigation.LastName;            

            string emailAddress = string.Empty;
            EmailAddresses objEmail =
                userFromRepo.EmployeeNoNavigation.EmailAddresses
                    .FirstOrDefault(o => o.IsPreffered == true);

            if (objEmail != null)
                emailAddress = Convert.ToString(objEmail.EmailAddress);

            int usergroupId = Convert.ToInt16(userFromRepo.UserGroupId);
            var roles = userFromRepo.UserGroup.UserGroupAccess.Where(o => o.UserGroupId == usergroupId);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, employeeNo),
                new Claim(ClaimTypes.GivenName, givenName),
                new Claim(ClaimTypes.Email, emailAddress),
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserDtoForLoginResponse>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token), user
            });
        }

        [HttpGet("ForgotPassword/{username}")]
        public async Task<IActionResult> ForgotPassword(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("Invalid username!");
            
            var recordFromRepo = await _authRepo.GetUser(username);
            if (recordFromRepo == null)
                return BadRequest("The user does not exist!");

            string password = recordFromRepo.Syek;

            string toEmail = string.Empty;
            EmailAddresses objEmail =
                recordFromRepo.EmployeeNoNavigation.EmailAddresses
                    .FirstOrDefault(o => o.IsPreffered == true);            
            if (objEmail != null)
                toEmail = objEmail.EmailAddress;

            // Enable when email notif is ready
            // SendPasswordToUser(username, password, toEmail);

            return Ok();

        }

        private async void SendPasswordToUser(string username, string password, string toEmail)
        {
            string subject = "System User Account";
            string body = "Your account has been created. Please use <br/> " + 
                "Username: " + username + "<br/>" + 
                "Password: " + password;

            await Helper.Utility.SendEmail(toEmail, subject, body, string.Empty);
        }

    }
}