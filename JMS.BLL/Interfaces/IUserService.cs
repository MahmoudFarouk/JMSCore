using JMS.BLL.Common;
using JMS.BLL.Models;
using JMS.DAL.Common.Enums;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string newpassword);
        PageResult<User> GetAll(string keywordfilter, PagingProperties pagingProperties);
        User GetById(Guid id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(Guid id);
        Role GetRoleById(Guid id);
        void ActivateDisactvate(Guid userId, bool isActive);
        ServiceResponse ChangePassword(Guid userId, string oldPassword, string newPassword);
        void ResetPassword(Guid userId, string randomPassword);
        User GetByName(string username);
        ServiceResponse ForgetPassword(string username, string email, string emailPassword, string hosting);
        ServiceResponse ResetForgetPassword(string token, string newpassword);
        


        public ServiceResponse<List<UserGroup>> GetUserGroups();
        public ServiceResponse<UserGroup> AddUserGroup(UserGroup group);
        public ServiceResponse EditUserGroup(UserGroup group);
        public ServiceResponse DeleteUserGroup(Guid groupId);
        public ServiceResponse<List<UserWorkForce>> GetUserWorkForces();
        public ServiceResponse AddUserWorkForce(UserWorkForce workforce);
        public ServiceResponse EditUserWorkForce(UserWorkForce workforce);
        public ServiceResponse DeleteUserWorkForce(Guid workforceId);

        public ServiceResponse<List<LookupModel<Guid>>> GetUsersByRole(UserRoles role);



    }
}
