using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.BLL.Models;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMS.BLL.Services
{
    public class RequestService : IRequestService
    {
        UserService _userService;
        private DatabaseContext _context;

        public RequestService(DatabaseContext context)
        {
            _context = context;
            _userService = new UserService(context);
        }

        public ServiceResponse<List<RequestModel>> GetUserRequests(Guid userId, UserRoles userRole)
        {
            ServiceResponse<List<RequestModel>> response = new ServiceResponse<List<RequestModel>>();

            try
            {
                if (userRole == UserRoles.Dispatcher)
                {
                    response.Data = GetDispatcherRequests(userId);
                }
                else if (userRole == UserRoles.JMC)
                {
                    response.Data = GetJMCRequests();
                }

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








        private List<RequestModel> GetJMCRequests()
        {
            return _context.Journey.Where(j =>
            j.JourneyStatus == JourneyStatus.PendingOnJMCInitialApproval ||
            j.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment ||
            j.JourneyStatus == JourneyStatus.PendingOnJMCInitialApproval
            //TODO the rest
            ).Select(j => new RequestModel
            {
                JourneyId = j.Id,
                JourneyTitle = j.Title,
                FromDestination = j.FromDestination,
                ToDestination = j.ToDestination,
                CreationDate = j.CreationDate,
                DeliveryDate = j.DeliveryDate
            }).OrderByDescending(r => r.CreationDate).ToList();
        }

        private List<RequestModel> GetDispatcherRequests(Guid dispatcherId)
        {
            return _context.Journey.Where(j => j.JourneyStatus == JourneyStatus.PendingOnDispatcherApproval && j.Dispatcher.Id == dispatcherId).Select(j => new RequestModel
            {
                JourneyId = j.Id,
                JourneyTitle = j.Title,
                FromDestination = j.FromDestination,
                ToDestination = j.ToDestination,
                CreationDate = j.CreationDate,
                DeliveryDate = j.DeliveryDate,
                Status = j.JourneyStatus
            }).OrderByDescending(r => r.CreationDate).ToList();
        }
    }
}
