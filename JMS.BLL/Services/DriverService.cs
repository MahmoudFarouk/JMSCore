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

        public ServiceResponse<List<User>> GetDrivers(string driverName = "")
        {
            ServiceResponse<List<User>> response = new ServiceResponse<List<User>>();

            try
            {
                if (!string.IsNullOrEmpty(driverName))
                {
                    response.Data = _context.Users.Where(u => u.UserRoles.Any(ur => ur.Role.Name == UserRoles.Driver.ToString()))
                                                  //.Where(u => string.IsNullOrEmpty(driverName) ? true : u.FullName.Contains(driverName)).ToList()
                                                  .Where(u => u.FullName.Contains(driverName)).ToList()
                                                  .Where(u => ((u.JourneyUpdates != null && u.JourneyUpdates.Count > 0) ? u.JourneyUpdates.Last().Date.HasValue && EF.Functions.DateDiffHour(DateTime.Now, u.JourneyUpdates.Last().Date) > 14 : true)).ToList();
                }
                else
                {
                    response.Data = _context.Users.Where(u => u.UserRoles.Any(ur => ur.Role.Name == UserRoles.Driver.ToString()))
                        .Where(u => string.IsNullOrEmpty(driverName) ? true : u.FullName.Contains(driverName)).ToList()
                                                  .Where(u => ((u.JourneyUpdates != null && u.JourneyUpdates.Count > 0) ? u.JourneyUpdates.Last().Date.HasValue && EF.Functions.DateDiffHour(DateTime.Now, u.JourneyUpdates.Last().Date) > 14 : true)).ToList();


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

        public ServiceResponse SubmitAssessment(int journeyId,int? journeyUpdateId, JourneyStatus status, List<AssessmentResult> assessmentResult)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                var journey = _context.Journey.Find(journeyId);
                journey.JourneyStatus = status;
                if (journeyUpdateId.HasValue)
                {
                    var journeyUpdate = _context.JourneyUpdate.Find(journeyUpdateId);
                    journeyUpdate.JourneyStatus = JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment;
                }
                if (assessmentResult.Count > 0)
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

        public ServiceResponse<List<AssessmentQuestion>> GetJourneyAssessment(int journeyId, bool isPostTrip = false, bool includeResults = false)
        {
            ServiceResponse<List<AssessmentQuestion>> response = new ServiceResponse<List<AssessmentQuestion>>();

            try
            {
                if (includeResults)
                {
                    if (isPostTrip)
                        response.Data = _context.AssessmentQuestion.Include(a => a.AssessmentResult).AsNoTracking()
                            .Where(q => (q.JourneyId == journeyId) && (q.CheckpointId == null) && (q.Category == QuestionCategory.PostTrip || q.Category == QuestionCategory.VehicleChecklist)).ToList();
                    else
                        response.Data = _context.AssessmentQuestion.Include(a => a.AssessmentResult).AsNoTracking()
                              .Where(q => (q.JourneyId == journeyId) && (q.CheckpointId == null) && (q.Category == QuestionCategory.PreTrip || q.Category == QuestionCategory.VehicleChecklist)).ToList();
                }
                else
                {
                    if (isPostTrip)
                        response.Data = _context.AssessmentQuestion
                            .Where(q => (q.JourneyId == journeyId) && (q.CheckpointId == null) && (q.Category == QuestionCategory.PostTrip || q.Category == QuestionCategory.VehicleChecklist)).ToList();
                    else
                        response.Data = _context.AssessmentQuestion
                            .Where(q => (q.JourneyId == journeyId) && (q.CheckpointId == null) && (q.Category == QuestionCategory.PreTrip || q.Category == QuestionCategory.VehicleChecklist)).ToList();
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

        public ServiceResponse<List<AssessmentQuestion>> GetCheckpointAssessment(int checkpointId, bool includeResults = false)
        {
            ServiceResponse<List<AssessmentQuestion>> response = new ServiceResponse<List<AssessmentQuestion>>();

            try
            {
                if (includeResults)
                    response.Data = _context.AssessmentQuestion.Include(a => a.AssessmentResult).AsNoTracking()
                        .Where(q => q.Category == QuestionCategory.CheckpointAssessment || q.Category == QuestionCategory.VehicleChecklist && q.CheckpointId == checkpointId).ToList();
                else
                    response.Data = _context.AssessmentQuestion
                            .Where(q => q.Category == QuestionCategory.CheckpointAssessment || q.Category == QuestionCategory.VehicleChecklist && q.CheckpointId == checkpointId).ToList();

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
