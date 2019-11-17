using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace JMS.BLL.Services
{
    public class DriverService : IDriverService
    {
        private DatabaseContext _context;

        public DriverService(DatabaseContext context)
        {
            _context = context;
        }

        public ServiceResponse<List<User>> GetDrivers(string driverName)
        {
            ServiceResponse<List<User>> response = new ServiceResponse<List<User>>();

            try
            {
                response.Data = (from user in _context.Users
                                 where
                                     user.UserRoles.Any(ur => ur.Role.Name == UserRoles.Driver.ToString()) &
                                     user.FullName.Contains(driverName) &
                                    (user.JourneyUpdates.LastOrDefault() != null && user.JourneyUpdates.LastOrDefault().Date.HasValue && EF.Functions.DateDiffHour(DateTime.Now, user.JourneyUpdates.LastOrDefault().Date) < 14)
                                 select user).ToList();

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

        public ServiceResponse SubmitAssessment(List<AssessmentResult> assessmentResult)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.AssessmentResult.AddRange(assessmentResult);
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

        public ServiceResponse SubmitStatus(JourneyUpdate journeyUpdate)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.JourneyUpdate.Add(journeyUpdate);
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
    }
}
