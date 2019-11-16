using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        PageResult<User> GetAll(string keywordfilter, PagingProperties pagingProperties);
        User GetById(Guid id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(Guid id);
        Role GetRoleById(Guid id);
        void ActivateDisactvate(Guid userId, bool isActive);
        ServiceResponse ChangePassword(Guid userId, string oldPassword, string newPassword);
        void ResetPassword(Guid userId, string randomPassword);

    }
}
