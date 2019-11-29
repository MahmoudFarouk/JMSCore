using JMS.BLL.Common;
using JMS.BLL.Models;
using JMS.DAL.Common.Enums;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface IRequestService
    {
        public ServiceResponse<List<RequestModel>> GetUserRequests(Guid userId, UserRoles userRole);
    }
}
