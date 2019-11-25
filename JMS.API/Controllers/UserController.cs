using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JMS.API.Constants;
using JMS.API.Models;
using JMS.BLL.Common;
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

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username.ToLower(), model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };
            var userRoles = user.UserRoles;
            var _roles = new List<RoleModel>();
            for (int i = 0; i < userRoles.Count; i++)
            {
                var role = _userService.GetRoleById(userRoles[i].RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
                _roles.Add(new RoleModel { Id = role.Id, Name = role.Name });
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
            var result = new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Token = tokenString,
                Roles = _roles
            };
            return Ok(new ServiceResponse<UserModel> { Data = result, Status = DAL.Common.Enums.ResponseStatus.Success });
        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("register")]
        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            // map dto to entity
            var user = new User
            {
                FullName = model.FullName,
                GatePassStatus = model.GatePassStatus,
                LicenseExpiryDate = model.LicenseExpiryDate,
                LicenseNo = model.LicenseNo,
                TrainingDetails = model.TrainingDetails,
                Username = model.Username.ToLower(),
                IsActive = true,
                UserRoles = new List<UserRole> { new UserRole { Id = Guid.NewGuid(), RoleId = model.RoleId } },
            };

            try
            {
                // save 
                _userService.Create(user, model.Password);
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success });
            }
            catch (AppException ex)
            {
                ex.LogException();
                // return error message if there was an exception
                return BadRequest(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError, Message = ex.Message });
            }
        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpGet]
        public IActionResult GetAll(string keywordfilter, PagingProperties pagingProperties)
        {
            try
            {
                var users = _userService.GetAll(keywordfilter, pagingProperties);
                var result = new ServiceResponse<PageResult<User>> { Data = users, Status = DAL.Common.Enums.ResponseStatus.Success };
                return Ok(result);
            }
            catch (AppException ex)
            {
                ex.LogException();
                // return error message if there was an exception
                return BadRequest(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
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
                var result = new ServiceResponse<UserModel> { Data = model, Status = DAL.Common.Enums.ResponseStatus.Success };
                return Ok(result);
            }
            catch (AppException ex)
            {
                ex.LogException();
                // return error message if there was an exception
                return BadRequest(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError, Message = ex.Message });
            }
        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, RegisterModel model)
        {
            var user = new User
            {
                Id = id,
                FullName = model.FullName,
                GatePassStatus = model.GatePassStatus,
                LicenseExpiryDate = model.LicenseExpiryDate,
                LicenseNo = model.LicenseNo,
                TrainingDetails = model.TrainingDetails,
                //Username = model.Username,
                UserRoles = string.IsNullOrEmpty(model.RoleId.ToString()) ? null : new List<UserRole> { new UserRole { RoleId = model.RoleId } },
            };

            try
            {
                // save 
                _userService.Update(user, model.Password);
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success });
            }
            catch (AppException ex)
            {
                ex.LogException();
                // return error message if there was an exception
                return BadRequest(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError, Message = ex.Message });
            }
        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success });
            }
            catch (AppException ex)
            {
                ex.LogException();
                // return error message if there was an exception
                return BadRequest(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError, Message = ex.Message });
            }

        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpPost()]
        [Route("ActivateDisActivate")]
        public IActionResult ActivateDisActivate(Guid id, bool isActive)
        {
            try
            {
                _userService.ActivateDisactvate(id, isActive);
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpPost()]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            try
            {
                var userid = Guid.Parse(User.Identity.Name);
                var result = _userService.ChangePassword(userid, model.OldPassword, model.NewPassword);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [Authorize(Roles = ConstRole.JMSAdmin)]
        [HttpPost()]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(Guid userid)
        {

            try
            {

                var password = General.CreatePassword(8);
                _userService.ResetPassword(userid, password);
                return Ok(new ServiceResponse<string> { Data = password, Status = DAL.Common.Enums.ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string username)
        {

            try
            {
                return Ok(_userService.ForgetPassword(username, _appSettings.Email, _appSettings.Password,_appSettings.WebUrl));
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });

            }

        }

        [HttpPost()]
        [AllowAnonymous]
        [Route("ResetForgetPassword")]
        public IActionResult ResetForgetPassword(string token, string newPassword)
        {
            try
            {
                return Ok(_userService.ResetForgetPassword(token, newPassword));
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.ServerError });
            }

        }

        [HttpGet()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("getgroups")]
        public IActionResult GetUserGroups()
        {
            return Ok(_userService.GetUserGroups());
        }

        [HttpGet()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("getworkforces")]
        public IActionResult GetUserWorkForces()
        {
            return Ok(_userService.GetUserWorkForces());
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("addgroup")]
        public IActionResult AddUserGroup(UserGroup group)
        {
            return Ok(_userService.AddUserGroup(group));
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("updategroup")]
        public IActionResult EditUserGroup(UserGroup group)
        {
            return Ok(_userService.EditUserGroup(group));
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("addworkforce")]
        public IActionResult AddUserWorkForce(UserWorkForce workforce)
        {
            return Ok(_userService.AddUserWorkForce(workforce));
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("updateworkforce")]
        public IActionResult EditUserGroup(UserWorkForce workforce)
        {
            return Ok(_userService.EditUserWorkForce(workforce));
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("deletegroup/{groupId}")]
        public IActionResult DeleteUserGroup(Guid groupId)
        {
            return Ok(_userService.DeleteUserGroup(groupId));
        }

        [HttpPost()]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("deleteworkforce/{workforceId}")]
        public IActionResult DeleteUserWorkForce(Guid workforceId)
        {
            return Ok(_userService.DeleteUserWorkForce(workforceId));
        }
    }
}