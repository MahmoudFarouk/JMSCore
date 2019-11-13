using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JMS.API.Constants;
using JMS.API.Models;
using JMS.BLL.Helper;
using JMS.BLL.Interfaces;
using JMS.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JMS.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        [Authorize(Roles =ConstRole.Dispatcher)]
        [Route("test")]
        [HttpPost("test")]
        public IActionResult test()
        {
            var extrarole = "";
            if (User.IsInRole(ConstRole.PL))
                extrarole = ConstRole.PL;
            return Ok(new { User.Identity.Name,extrarole });
        }
        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(UserModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };
            var userRoles = user.UserRoles;
            for(int i = 0; i < userRoles.Count; i++)
            {
                var role = _userService.GetRoleById(userRoles[i].Id);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                user.Id,
                user.Username,
                user.FullName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost("register")]
        public IActionResult Register(UserModel model)
        {
            // map dto to entity
            var user = new User
            {
                FullName = model.FullName,
                GatePassStatus = model.GatePassStatus,
                LicenseExpiryDate = model.LicenseExpiryDate,
                LicenseNo = model.LicenseNo,
                TrainingDetails = model.TrainingDetails,
                Username = model.Username
            };

            try
            {
                // save 
                _userService.Create(user, model.Password);
                return Ok(user);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserModel>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            var model = new UserModel
            {
                FullName = user.FullName,
                GatePassStatus = user.GatePassStatus,
                LicenseExpiryDate = user.LicenseExpiryDate,
                LicenseNo = user.LicenseNo,
                TrainingDetails = user.TrainingDetails,
                Username = user.Username
            };
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UserModel model)
        {
            var user = new User
            {
                FullName = model.FullName,
                GatePassStatus = model.GatePassStatus,
                LicenseExpiryDate = model.LicenseExpiryDate,
                LicenseNo = model.LicenseNo,
                TrainingDetails = model.TrainingDetails,
                Username = model.Username
            };

            user.Id = id;
            try
            {
                // save 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}