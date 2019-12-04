using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.BLL.Models;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FlexLabs.EntityFrameworkCore.Upsert;

namespace JMS.BLL.Services
{
    public class JourneyService : IJourneyService
    {
        private DatabaseContext _context;

        public JourneyService(DatabaseContext context)
        {
            _context = context;
        }

        public ServiceResponse InitiateJourney(Journey journey)
        {
            journey.CreationDate = DateTime.Now;

            //TODO ( Khamis should do this after driver selection)
            //journey.JourneyStatus = JourneyStatus.PendingOnDriverCompleteCheckpointAssessment;
            journey.JourneyStatus = journey.IsThirdParty ? JourneyStatus.PendingOnDispatcherApproval : JourneyStatus.PendingOnJMCInitialApproval;

            var fromCheckpoint = _context.Checkpoint.FirstOrDefault(c => c.Latitude == journey.FromLat && c.Longitude == journey.FromLng && c.IsThirdParty == journey.IsThirdParty);

            foreach (var checkpoint in journey.Checkpoints)
            {
                var dbCheckpoint = _context.Checkpoint.FirstOrDefault(c => c.Latitude == checkpoint.Latitude && c.Longitude == checkpoint.Longitude);

                if (dbCheckpoint == null && checkpoint.Id == 0)
                {
                    checkpoint.IsThirdParty = journey.IsThirdParty;
                    _context.Add(checkpoint);
                }
                else
                    checkpoint.Id = dbCheckpoint.Id;

                journey.JourneyUpdates.Add(new JourneyUpdate
                {
                    Checkpoint = checkpoint.Id == 0 ? checkpoint : null,
                    CheckpointId = checkpoint.Id == 0 ? 0 : checkpoint.Id,
                    Latitude = checkpoint.Latitude,
                    Longitude = checkpoint.Longitude,
                    Date = DateTime.Now,
                    JourneyStatus = journey.JourneyStatus,
                    IsJourneyCheckpoint = true,
                    UserId = journey.UserId
                });
            }

            _context.Journey.Add(journey);
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }

        public ServiceResponse ValidateJourney(Journey journey)
        {

            var currentJourney = _context.Journey.Include(j => j.JourneyUpdates).FirstOrDefault(j => j.Id == journey.Id);
            _context.JourneyUpdate.RemoveRange(currentJourney.JourneyUpdates);
            _context.Journey.Remove(currentJourney);
            _context.SaveChanges();

            journey.CreationDate = DateTime.Now;
            journey.AssessmentQuestion.ToList().ForEach(q => q.Id = 0);

            journey.Id = 0;
            journey.JourneyStatus = JourneyStatus.PendingOnDriverSelection;

            var fromCheckpoint = _context.Checkpoint.FirstOrDefault(c => c.Latitude == journey.FromLat && c.Longitude == journey.FromLng && c.IsThirdParty == journey.IsThirdParty);

            foreach (var checkpoint in journey.Checkpoints)
            {
                var dbCheckpoint = _context.Checkpoint.FirstOrDefault(c => c.Latitude == checkpoint.Latitude && c.Longitude == checkpoint.Longitude);

                if (dbCheckpoint == null && checkpoint.Id == 0)
                {
                    checkpoint.IsThirdParty = journey.IsThirdParty;
                    _context.Add(checkpoint);
                }
                else
                    checkpoint.Id = dbCheckpoint.Id;

                journey.JourneyUpdates.Add(new JourneyUpdate
                {
                    Checkpoint = checkpoint.Id == 0 ? checkpoint : null,
                    CheckpointId = checkpoint.Id == 0 ? 0 : checkpoint.Id,
                    Latitude = checkpoint.Latitude,
                    Longitude = checkpoint.Longitude,
                    Date = DateTime.Now,
                    JourneyStatus = JourneyStatus.PendingOnDriverCompleteCheckpointAssessment,
                    IsJourneyCheckpoint = true,
                    UserId = journey.UserId,
                });
            }

            _context.Journey.Add(journey);
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success, Message = journey.Id.ToString() };
        }


        public ServiceResponse UpdateJourney(Journey journey)
        {
            var _journey = _context.Journey.Find(journey.Id);
            _journey.CargoPriority = journey.CargoPriority;
            _journey.CargoSeverity = journey.CargoSeverity;
            _journey.CargoType = journey.CargoType;
            _journey.CargoWeight = journey.CargoWeight;
            _journey.DeliveryDate = journey.DeliveryDate;
            _journey.FromDestination = journey.FromDestination;
            _journey.FromLat = journey.FromLat;
            _journey.FromLng = journey.FromLng;
            _journey.IsThirdParty = journey.IsThirdParty;
            _journey.IsTruckTransport = journey.IsTruckTransport;
            _journey.JourneyStatus = journey.JourneyStatus;
            _journey.StartDate = journey.StartDate;
            _journey.Title = journey.Title;
            _journey.ToDestination = journey.ToDestination;
            _journey.ToLat = journey.ToLat;
            _journey.ToLng = journey.ToLng;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }

        public ServiceResponse<object> GetJourneyDetails(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            var assesments = new List<AssessmentQuestion>();

            if (journey.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverPreTripAssessment ||
                journey.JourneyStatus == JourneyStatus.PendingOnGBMJourneyApprovalPreTripAssessment ||
                journey.JourneyStatus == JourneyStatus.PendingOnQHSEJourneyApprovalPreTripAssessment)
            {

                assesments = _context.AssessmentQuestion.Where(x => x.JourneyId == journeyId && (x.Category == QuestionCategory.PreTrip || x.Category == QuestionCategory.VehicleChecklist)).Include(x => x.AssessmentResult).Select(
                    x => new AssessmentQuestion
                    {
                        Category = x.Category,
                        CheckpointId = x.CheckpointId,
                        Id = x.Id,
                        IsThirdParty = x.IsThirdParty,
                        JourneyId = x.JourneyId,
                        Question = x.Question,
                        Weight = x.Weight,
                        AssessmentResult = x.AssessmentResult.Where(c => c.Category == QuestionCategory.PreTrip)
                    }
                    ).AsNoTracking().ToList();
            }
            else if (journey.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment)
            {
                var checkpoint = _context.JourneyUpdate.Where(x => x.JourneyId == journeyId && x.CheckpointId != null && x.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment).OrderByDescending(x => x.Id).FirstOrDefault();
                assesments = _context.AssessmentQuestion.
                    Where(x => x.JourneyId == journeyId && ((x.Category == QuestionCategory.CheckpointAssessment && x.CheckpointId == checkpoint.CheckpointId) || x.Category == QuestionCategory.VehicleChecklist)).
                    Include(x => x.AssessmentResult)
                    .Include(z => z.Checkpoint).Select(x => new AssessmentQuestion
                    {
                        Checkpoint = x.Checkpoint,
                        Category = x.Category,
                        CheckpointId = x.CheckpointId,
                        Id = x.Id,
                        IsThirdParty = x.IsThirdParty,
                        JourneyId = x.JourneyId,
                        Question = x.Question,
                        Weight = x.Weight,
                        AssessmentResult = x.AssessmentResult.Where(c => c.Category == QuestionCategory.CheckpointAssessment && c.CheckPointId == checkpoint.CheckpointId)
                    }).AsNoTracking().ToList();
            }
            else if (journey.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverPostTripAssessment ||
               journey.JourneyStatus == JourneyStatus.PendingOnDispatcherApproveDriverPostTripAssessment)
            {

                assesments = _context.AssessmentQuestion.Where(x => x.JourneyId == journeyId && (x.Category == QuestionCategory.PostTrip || x.Category == QuestionCategory.VehicleChecklist)).Include(x => x.AssessmentResult).Select(
                    x => new AssessmentQuestion
                    {
                        Category = x.Category,
                        CheckpointId = x.CheckpointId,
                        Id = x.Id,
                        IsThirdParty = x.IsThirdParty,
                        JourneyId = x.JourneyId,
                        Question = x.Question,
                        Weight = x.Weight,
                        AssessmentResult = x.AssessmentResult.Where(c => c.Category == QuestionCategory.PostTrip)
                    }
                    ).AsNoTracking().ToList();
            }

            var details = new
            {
                CargoPriority = journey.CargoPriority,
                CargoSeverity = journey.CargoSeverity,
                CargoType = journey.CargoType,
                CargoWeight = journey.CargoWeight,
                DeliveryDate = journey.DeliveryDate,
                FromDestination = journey.FromDestination,
                FromLat = journey.FromLat.Value,
                FromLng = journey.FromLng.Value,
                Id = journey.Id,
                IsThirdParty = journey.IsThirdParty,
                IsTruckTransport = journey.IsTruckTransport,
                JourneyStatus = journey.JourneyStatus,
                StartDate = journey.StartDate,
                Title = journey.Title,
                ToDestination = journey.ToDestination,
                ToLat = journey.ToLat.HasValue ? journey.ToLat.Value : default(double?),
                ToLng = journey.ToLng.HasValue ? journey.ToLng.Value : default(double?),
                UserId = journey.UserId,
                UserFullname = journey.UserId != null ? _context.Users.Find(journey.UserId).FullName : "",
                Assesments = assesments



            };
            return new ServiceResponse<object>
            {
                Data = details,
                Status = ResponseStatus.Success
            };
        }
        public ServiceResponse<PageResult<Journey>> GetJourneys(DateTime? date, PagingProperties pagingProperties)
        {
            var skip = (pagingProperties.PageNo - 1) * pagingProperties.PageSize;
            IQueryable<Journey> items = _context.Journey;
            var totalItems = items.Count();
            if (date.HasValue)
            {
                var _date = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day);
                items = items.Where(x => x.StartDate > _date && x.StartDate < _date.AddHours(24));
            }
            items = items.Skip(skip).Take(pagingProperties.PageSize);
            var model = new PageResult<Journey> { PageItems = items.ToList(), TotalItems = totalItems };
            return new ServiceResponse<PageResult<Journey>> { Data = model, Status = ResponseStatus.Success };

        }
        public ServiceResponse AssignJourneyDriverVehicle(int journeyId, Guid driverId, string vehcileNo)
        {
            var journeyupdate = _context.JourneyUpdate.FirstOrDefault(x => x.JourneyId == journeyId && x.DriverId == driverId);
            journeyupdate.VehicleNo = vehcileNo;
            journeyupdate.JourneyStatus = JourneyStatus.PendingOnDriverCompletePreTripAssessment;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };

        }

        public ServiceResponse UpdateJourneyCheckpoint(int journeyUpdateId, JourneyStatus status)
        {
            var journey = _context.JourneyUpdate.Find(journeyUpdateId);
            journey.JourneyStatus = status;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }

        public ServiceResponse<int> AssaignDreiverJourneyUpdate(JourneyUpdate JourneyUpdate)
        {
            var journey = _context.Journey.Find(JourneyUpdate.JourneyId);
            journey.JourneyStatus = JourneyStatus.PendingOnDriverCompletePreTripAssessment;
            _context.Notification.Add(new Notification
            {
                CreationTime = DateTime.Now,
                Text = "Journey from " + journey.FromDestination + " to " + journey.ToDestination + " Assaigned to you",
                UserId = JourneyUpdate.DriverId.Value
            });
            if (JourneyUpdate.Id > 0)
            {
                var item = _context.JourneyUpdate.Find(JourneyUpdate.Id);
                item.VehicleNo = JourneyUpdate.VehicleNo;
                item.DriverId = JourneyUpdate.DriverId;
                item.StatusMessage = JourneyUpdate.StatusMessage;
                item.Date = DateTime.Now;
                _context.SaveChanges();
                return new ServiceResponse<int> { Data = item.Id, Status = ResponseStatus.Success };
            }
            else
            {
                var item = _context.JourneyUpdate.Add(JourneyUpdate);
                _context.SaveChanges();
                return new ServiceResponse<int> { Data = item.Entity.Id, Status = ResponseStatus.Success };
            }

        }

        public Journey GetById(int id)
        {
            return _context.Journey.Find(id);
        }
        public JourneyUpdate GetJourneyUpdateDriverInfo(int journeyId)
        {
            return _context.JourneyUpdate.FirstOrDefault(x => x.JourneyId == journeyId && x.DriverId != null);
        }

        public ServiceResponse<List<Journey>> GetUserRequests(UserRoles userRole)
        {
            ServiceResponse<List<Journey>> response = new ServiceResponse<List<Journey>>();

            try
            {

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

        public ServiceResponse<object> JourneyCheckPoints(int journeyId)
        {
            throw new NotImplementedException();
        }
        public ServiceResponse UpdateJourneyStatus(int journeyId, JourneyStatus status)
        {
            var journey = _context.Journey.Find(journeyId);
            var countjourneyUpdates = _context.JourneyUpdate.Where(x => x.JourneyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment && x.JourneyId == journeyId).Count();
            if (journey.JourneyStatus == JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment && countjourneyUpdates == 1)
            {
                var ju = _context.JourneyUpdate.Where(x => x.JourneyStatus == JourneyStatus.PendingOnDriverCompleteCheckpointAssessment && x.JourneyId == journeyId).FirstOrDefault();
                ju.JourneyStatus = JourneyStatus.PendingOnJMCApproveDriverCheckpointAssessment;
                journey.JourneyStatus = JourneyStatus.PendingOnDriverCompletePostTripAssessment;
            }
            else
                journey.JourneyStatus = status;

            if (countjourneyUpdates == 0)
                journey.JourneyStatus = JourneyStatus.PendingOnDriverCompletePostTripAssessment;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }
        public ServiceResponse AddJourneyUpdate(JourneyUpdate JourneyUpdate)
        {
            JourneyUpdate.Date = DateTime.Now;
            _context.JourneyUpdate.Add(JourneyUpdate);
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }


        public ServiceResponse<Journey> GetAllJourneyInfo(int journeyId)
        {
            ServiceResponse<Journey> response = new ServiceResponse<Journey>();

            try

            {
                var currentJourney = _context.Journey.AsNoTracking().Include(j => j.JourneyUpdates).Include(j => j.AssessmentQuestion).ThenInclude(q=>q.AssessmentResult).FirstOrDefault(j => j.Id == journeyId);

                currentJourney.Checkpoints = new LinkedList<Checkpoint>();
                foreach (var update in currentJourney.JourneyUpdates)
                {
                    if (update.IsJourneyCheckpoint)
                    {
                        var checkpoint = _context.Checkpoint.Find(update.CheckpointId);
                        currentJourney.Checkpoints.Add(checkpoint);
                    }                    
                }

                response.Data = currentJourney;

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

        public ServiceResponse<List<JourneyUpdate>> GetJourneyMontoring(int journeyId)
        {
            var data = _context.JourneyUpdate.Where(x => x.JourneyId == journeyId && !x.IsJourneyCheckpoint).Include(x => x.Journey).AsNoTracking().ToList();
            return new ServiceResponse<List<JourneyUpdate>> { Data = data, Status = ResponseStatus.Success };
        }









        //public ServiceResponse ApproveJourney(int journeyId)
        //{
        //    var journey = _context.Journey.Find(journeyId);
        //    journey.JourneyStatus = JourneyStatus.JMCApprovedJourney;
        //    _context.SaveChanges();
        //    return new ServiceResponse { Status = ResponseStatus.Success };

        //}

        //public ServiceResponse CloseJourney(int journeyId)
        //{
        //    var journey = _context.Journey.Find(journeyId);
        //    journey.JourneyStatus = JourneyStatus.Closed;
        //    _context.SaveChanges();
        //    return new ServiceResponse { Status = ResponseStatus.Success };
        //}

        //public ServiceResponse CompleteJourney(int journeyId)
        //{
        //    var journey = _context.Journey.Find(journeyId);
        //    journey.JourneyStatus = JourneyStatus.Completed;
        //    _context.SaveChanges();
        //    return new ServiceResponse { Status = ResponseStatus.Success };
        //}

        //public ServiceResponse StopJourney(int journeyId)
        //{
        //    var journey = _context.Journey.Find(journeyId);
        //    journey.JourneyStatus = JourneyStatus.Stopped;
        //    _context.SaveChanges();
        //    return new ServiceResponse { Status = ResponseStatus.Success };
        //}

    }
}
