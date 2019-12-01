using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.BLL.Models;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using Microsoft.EntityFrameworkCore;
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
                else if (userRole == UserRoles.JMC || userRole == UserRoles.QHSE || userRole == UserRoles.GBM || userRole == UserRoles.OperationManager)
                {
                    response.Data = GetJMCRequests();
                }
                else if (userRole == UserRoles.Driver)
                {
                    response.Data = GetDriverRequests(userId);
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
                        j.JourneyStatus == JourneyStatus.PendingOnDriverSelection ||
                        j.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment ||
                        j.JourneyStatus == JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment ||
                        j.JourneyStatus == JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment ||
                        j.JourneyStatus == JourneyStatus.DriverStartedJourney ||
                        j.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment ||
                        j.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment ||
                        j.JourneyStatus == JourneyStatus.JourneyCompleted ||
                        j.JourneyStatus == JourneyStatus.JourneyPaused ||
                        j.JourneyStatus == JourneyStatus.JourneyStopped
            ).Select(j => new RequestModel
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

        private List<RequestModel> GetDispatcherRequests(Guid dispatcherId)
        {
            return _context.Journey.Where(j => j.Dispatcher.Id == dispatcherId && (
                        j.JourneyStatus == JourneyStatus.PendingOnDispatcherApproval ||
                        j.JourneyStatus == JourneyStatus.PendingOnDriverSelection ||
                        j.JourneyStatus == JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment) ||
                        j.JourneyStatus == JourneyStatus.JourneyCompleted ||
                        j.JourneyStatus == JourneyStatus.JourneyPaused ||
                        j.JourneyStatus == JourneyStatus.JourneyStopped
            ).Select(j => new RequestModel
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

        private List<RequestModel> GetDriverRequests(Guid driverId)
        {
            return _context.Journey.Include(j => j.JourneyUpdates).Where(j =>
                         j.JourneyUpdates.Any(u => u.DriverId == driverId) && (
                         j.JourneyStatus == JourneyStatus.PendingOnDriverStartJourney ||
                         j.JourneyStatus == JourneyStatus.DriverStartedJourney ||
                         j.JourneyStatus == JourneyStatus.PendingOnDriverCompletePreTripAssessment ||
                         j.JourneyStatus == JourneyStatus.PendingOnDriverCompletePostTripAssessment ||
                         j.JourneyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment))
                        .Select(j => new RequestModel
                        {
                            JourneyId = j.Id,
                            JourneyTitle = j.Title,
                            FromDestination = j.FromDestination,
                            ToDestination = j.ToDestination,
                            CreationDate = j.CreationDate,
                            DeliveryDate = j.DeliveryDate,
                            Status = j.JourneyStatus
                        }).OrderByDescending(r => r.CreationDate).ToList();

            //return _context.JourneyUpdate.Include(j => j.Journey).Where(j => j.DriverId == driverId &&
            //              j.JourneyStatus == JourneyStatus.DriverStartedJourney ||
            //              j.JourneyStatus == JourneyStatus.PendingOnDriverCompletePreTripAssessment ||
            //              j.JourneyStatus == JourneyStatus.PendingOnDriverCompletePostTripAssessment ||
            //              j.JourneyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment
            //                ).Select(j => new RequestModel
            //                {
            //                    JourneyId = j.Journey.Id,
            //                    JourneyTitle = j.Journey.Title,
            //                    FromDestination = j.Journey.FromDestination,
            //                    ToDestination = j.Journey.ToDestination,
            //                    CreationDate = j.Journey.CreationDate,
            //                    DeliveryDate = j.Journey.DeliveryDate,
            //                    Status = j.JourneyStatus
            //                }).OrderByDescending(r => r.CreationDate).ToList();




        }

    }
}
