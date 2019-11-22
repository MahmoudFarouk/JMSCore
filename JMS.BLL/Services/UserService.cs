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

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // update user properties
            user.FullName = userParam.FullName;
            user.GatePassStatus = userParam.GatePassStatus;
            user.LicenseExpiryDate = userParam.LicenseExpiryDate;
            user.LicenseNo = user.LicenseNo;
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
                return new ServiceResponse { Status = ResponseStatus.Success };

            }
            else
            {
                return new ServiceResponse { Status = ResponseStatus.OldPasswordWrong };
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


        public ServiceResponse AddUserGroup(UserGroup group)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.UserGroups.Add(group);
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

        public ServiceResponse AddUserWorkForce(UserWorkForce workforce)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.UserWorkForces.Add(workforce);
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

        public ServiceResponse DeleteUserGroup(Guid groupId)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.UserGroups.Remove(_context.UserGroups.Find(groupId));
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
                _context.UserWorkForces.Remove(_context.UserWorkForces.Find(workforceId));
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
                response.Data = _context.UserGroups.ToList();

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
                response.Data = _context.UserWorkForces.ToList();

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

    }
}
