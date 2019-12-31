using JMS.BLL.Interfaces;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JMS.BLL.Helper;
using Microsoft.EntityFrameworkCore;
using JMS.BLL.Common;
using JMS.DAL.Common.Enums;
using System.Net.Mail;
using System.Net;
using JMS.BLL.Models;

namespace JMS.BLL.Services
{
    public class UserService : IUserService
    {
        private DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.Include(x => x.UserRoles).SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public PageResult<User> GetAll(string keywordfilter, PagingProperties pagingProperties)
        {
            var skip = (pagingProperties.PageNo - 1) * pagingProperties.PageSize;
            IQueryable<User> items = _context.Users;
            var totalItems = items.Count();
            if (!string.IsNullOrEmpty(keywordfilter))
                items = items.Where(x => x.FullName.Contains(keywordfilter) || x.Username.Contains(keywordfilter));
            items = items.Skip(skip).Take(pagingProperties.PageSize);
            return new PageResult<User> { PageItems = items.ToList(), TotalItems = totalItems };
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User updatedUser, string password = null)
        {
            var user = _context.Users.Include(u => u.UserRoles).FirstOrDefault(u => u.Id == updatedUser.Id);


            if (user == null)
                throw new AppException("User not found");

            //if (updatedUser.Username != user.Username)
            //{
            //    // username has changed so check if the new username is already taken
            //    if (_context.Users.Any(x => x.Username == updatedUser.Username))
            //        throw new AppException("Username " + updatedUser.Username + " is already taken");
            //}

            // update user properties
            //user.Username = updatedUser.Username;
            user.FullName = updatedUser.FullName;
            user.GatePassStatus = updatedUser.GatePassStatus;
            user.LicenseExpiryDate = updatedUser.LicenseExpiryDate;
            user.LicenseNo = updatedUser.LicenseNo;
            user.UserGroupId = updatedUser.UserGroupId;
            user.UserWorkForceId = updatedUser.UserWorkForceId;

            if (updatedUser.UserRoles != null && user.UserRoles != null)
            {
                user.UserRoles.First().RoleId = updatedUser.UserRoles.First().RoleId;
            }

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public Role GetRoleById(Guid id)
        {
            return _context.Roles.Find(id);
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public void ActivateDisactvate(Guid userId, bool isActive)
        {
            var user = _context.Users.Find(userId);
            user.IsActive = isActive;
            _context.SaveChanges();

        }

        public ServiceResponse ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var user = _context.Users.Find(userId);
            if (VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success };

            }
            else
            {
                return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.OldPasswordWrong };
            }

        }

        public void ResetPassword(Guid userId, string randomPassword)
        {
            var user = _context.Users.Find(userId);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(randomPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

        }

        public User GetByName(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username == username.ToLower());
        }
        public ServiceResponse ForgetPassword(string username, string email, string emailPassword, string hosting)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username.ToLower());
            if (user == null)
                return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.UsernameNotExsit };
            user.ChangePasswordToken = Guid.NewGuid().ToString() + General.CreatePassword(8);
            _context.SaveChanges();
            var fromAddress = new MailAddress(email, "JMS Support");
            var toAddress = new MailAddress(username, "JMS Worker");
            string fromPassword = emailPassword;
            const string subject = "Forget Password";
            string body = "<a href='" + hosting + "forgetchangepassword?token=" + user.ChangePasswordToken + "'>Reset your password</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success };

        }
        public ServiceResponse ResetForgetPassword(string token, string newpassword)
        {
            var user = _context.Users.FirstOrDefault(x => x.ChangePasswordToken == token);
            if (user == null)
                return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Failed };
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(newpassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ChangePasswordToken = null;
            _context.SaveChanges();
            return new ServiceResponse { Status = DAL.Common.Enums.ResponseStatus.Success };
        }


        public ServiceResponse<UserGroup> AddUserGroup(UserGroup group)
        {
            ServiceResponse<UserGroup> response = new ServiceResponse<UserGroup>();

            try
            {
                var groups = _context.UserGroups.Where(x => x.Name.ToLower() == group.Name.ToLower() && !x.IsDeleted);
                var groupsCount = groups.Count();
                if (groupsCount > 0)
                {
                    response.Status = ResponseStatus.UsernameNotExsit;
                    response.Data = groups.FirstOrDefault();
                    return response;
                }
                var _group = _context.UserGroups.Add(group);
                _context.SaveChanges();
                response.Data = _group.Entity;
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;
        }

        public ServiceResponse<UserWorkForce> AddUserWorkForce(UserWorkForce workforce)
        {
            ServiceResponse<UserWorkForce> response = new ServiceResponse<UserWorkForce>();

            try
            {
                var groups = _context.UserWorkForces.Where(x => x.Name.ToLower() == workforce.Name.ToLower() && !x.IsDeleted);
                var groupsCount = groups.Count();
                if (groupsCount > 0)
                {
                    response.Status = ResponseStatus.UsernameNotExsit;
                    response.Data = groups.FirstOrDefault();
                    return response;
                }
                var _group = _context.UserWorkForces.Add(workforce);
                _context.SaveChanges();
                response.Data = _group.Entity;
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;
        }

        public ServiceResponse DeleteUserGroup(Guid groupId)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var group = _context.UserGroups.Find(groupId);
                group.IsDeleted = true;
                _context.SaveChanges();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;

        }

        public ServiceResponse DeleteUserWorkForce(Guid workforceId)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var group = _context.UserWorkForces.Find(workforceId);
                group.IsDeleted = true;
                _context.SaveChanges();
                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;
        }

        public ServiceResponse EditUserGroup(UserGroup group)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.UserGroups.Update(group);

                _context.SaveChanges();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;
        }

        public ServiceResponse EditUserWorkForce(UserWorkForce workforce)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.UserWorkForces.Update(workforce);
                _context.SaveChanges();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;
        }

        public ServiceResponse<List<UserGroup>> GetUserGroups()
        {
            ServiceResponse<List<UserGroup>> response = new ServiceResponse<List<UserGroup>>();

            try
            {
                response.Data = _context.UserGroups.Where(x => !x.IsDeleted).AsNoTracking().ToList();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;

        }

        public ServiceResponse<List<UserWorkForce>> GetUserWorkForces()
        {
            ServiceResponse<List<UserWorkForce>> response = new ServiceResponse<List<UserWorkForce>>();

            try
            {
                response.Data = _context.UserWorkForces.Where(x => !x.IsDeleted).AsNoTracking().ToList();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;
        }



        public ServiceResponse<List<LookupModel<Guid>>> GetUsersByRole(UserRoles role)
        {
            ServiceResponse<List<LookupModel<Guid>>> response = new ServiceResponse<List<LookupModel<Guid>>>();

            try
            {
                response.Data = _context.Users.Where(u => u.UserRoles.Any(r => r.Role.Name == role.ToString())).Select(u => new LookupModel<Guid>
                {
                    Id = u.Id,
                    Value = u.FullName
                }).ToList();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }
            return response;
        }

        public ServiceResponse<object> GetAllUsers()
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == "Driver");
            var data = _context.Users.Include(x => x.UserRoles).ToList().Where(x=>x.UserRoles[0].RoleId==role.Id)
                //.ToList();
                .Where(x => !x.IsDeleted).Select(x => new
                {
                    x.Id,
                    RoleName = x.UserRoles != null && x.UserRoles.Count > 0 && x.UserRoles[0].RoleId != null ? _context.Roles.Find(x.UserRoles[0].RoleId).Name : "",
                    x.FullName,
                    x.GatePassStatus,
                    x.IsActive,
                    x.LicenseExpiryDate,
                    x.LicenseNo,
                    x.TrainingDetails,
                    x.UserWorkForceId,
                    x.Username,
                    x.UserGroupId,
                    WorForceTitle = x.UserWorkForce != null ? x.UserWorkForce.Name : "",
                    TeamName = x.UserGroup != null ? x.UserGroup.Name : ""
                });
            return new ServiceResponse<object> { Data = data, Status = ResponseStatus.Success };
        }
    }
}
