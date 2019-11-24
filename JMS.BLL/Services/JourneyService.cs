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
            _context.Journey.Add(journey);
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
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
            _journey.ToDistination = journey.ToDistination;
            _journey.ToLat = journey.ToLat;
            _journey.ToLng = journey.ToLng;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };


        }
        public ServiceResponse<object> GetJourneyDetails(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            var assesements = _context.AssessmentQuestion.Where(x => x.JourneyId == journeyId).Include(x => x.AssessmentResult).ToList().Select(c =>
                new
                {
                    c.Category,
                    c.Question,
                    Assessments = c.AssessmentResult.Select(x => new { x.IsYes })
                }
                ).GroupBy(x => x.Category);

            //var journeyUpdates = _context.JourneyUpdate.Include(x => x.Checkpoint).Include(x => x.AssessmentResult).Where(x => x.JourneyId == journeyId).ToList();
            //var _journeyUpdates = new List<JourneyUpdateModel>();
            //var _checkpoints = new List<CheckPointModel>();
            //var _assessmentQuestion = new List<AssessmentQuestionModel>();
            //for (int i = 0; i < journeyUpdates.Count(); i++)
            //{
            //    var journeyUpdate = journeyUpdates[i];

            //    if (journeyUpdate.CheckpointId != null)
            //    {
            //        _checkpoints.Add(new CheckPointModel
            //        {
            //            Id = journeyUpdate.Checkpoint.Id,
            //            IsThirdParty = journeyUpdate.Checkpoint.IsThirdParty,
            //            Lat = journeyUpdate.Checkpoint.Latitude.HasValue ? journeyUpdate.Checkpoint.Latitude.Value : default(double?),
            //            Lng = journeyUpdate.Checkpoint.Longitude.HasValue ? journeyUpdate.Checkpoint.Longitude.Value : default(double?),
            //            Name = journeyUpdate.Checkpoint.Name
            //        });
            //    }
            //    var AssessmentResults = journeyUpdate.AssessmentResult.ToList();
            //    for (var x = 0; x < AssessmentResults.Count; x++)
            //    {
            //        var assessmentResult = AssessmentResults[x];
            //        var question = _context.AssessmentQuestion.Find(assessmentResult.QuestionId);
            //        var _question = _assessmentQuestion.Where(x => x.Id == assessmentResult.QuestionId).FirstOrDefault();
            //        if (_question != null)
            //        {
            //            _question.AssessmentResults.Add(new AssessmentResultModel
            //            {
            //                Comment = assessmentResult.Comment,
            //                Id = assessmentResult.Id,
            //                IsYes = assessmentResult.IsYes,
            //                JourneyUpdateId = assessmentResult.JourneyUpdateId,
            //                QuestionId = assessmentResult.QuestionId,
            //                SubmittedBy = assessmentResult.UserId,
            //                SubmittedByname = assessmentResult.UserId != null ? _context.Users.Find(assessmentResult.UserId).FullName : "",
            //                VehicleNo = assessmentResult.VehicleNo
            //            });
            //        }
            //        else
            //        {
            //            var _assessResults = new List<AssessmentResultModel>();
            //            _assessResults.Add(new AssessmentResultModel
            //            {
            //                Comment = assessmentResult.Comment,
            //                Id = assessmentResult.Id,
            //                IsYes = assessmentResult.IsYes,
            //                JourneyUpdateId = assessmentResult.JourneyUpdateId,
            //                QuestionId = assessmentResult.QuestionId,
            //                SubmittedBy = assessmentResult.UserId,
            //                SubmittedByname = assessmentResult.UserId != null ? _context.Users.Find(assessmentResult.UserId).FullName : "",
            //                VehicleNo = assessmentResult.VehicleNo
            //            });
            //            _assessmentQuestion.Add(new AssessmentQuestionModel
            //            {
            //                AssessmentResults = _assessResults,
            //                Category = question.Category,
            //                CheckpointId = question.CheckpointId,
            //                Id = question.Id,
            //                IsThirdParty = question.IsThirdParty,
            //                Question = question.Question,
            //                Weight = question.Weight
            //            });
            //        }
            //    }
            //    _journeyUpdates.Add(new JourneyUpdateModel
            //    {
            //        CheckpointId = journeyUpdate.CheckpointId,
            //        Date = journeyUpdate.Date,
            //        DriverId = journeyUpdate.DriverId.ToString(),
            //        Drivername = journeyUpdate.DriverId != null ? _context.Users.Find(journeyUpdate.DriverId).FullName : "",
            //        Id = journeyUpdate.Id,
            //        IsAlert = journeyUpdate.IsAlert,
            //        IsDriverStatus = journeyUpdate.IsDriverStatus,
            //        IsJourneyCheckpoint = journeyUpdate.IsJourneyCheckpoint,
            //        JourneyId = journeyUpdate.JourneyId,
            //        JourneyStatus = journeyUpdate.JourneyStatus,
            //        Latitude = journeyUpdate.Latitude.HasValue ? journeyUpdate.Latitude.Value : default(double?),
            //        Longitude = journeyUpdate.Longitude.HasValue ? journeyUpdate.Longitude.Value : default(double?),
            //        RiskLevel = journeyUpdate.RiskLevel,
            //        StatusMessage = journeyUpdate.StatusMessage,
            //        VehicleNo = journeyUpdate.VehicleNo,
            //        AssessmentQuestions = _assessmentQuestion,

            //    });


            //}
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
                ToDistination = journey.ToDistination,
                ToLat = journey.ToLat.HasValue ? journey.ToLat.Value : default(double?),
                ToLng = journey.ToLng.HasValue ? journey.ToLng.Value : default(double?),
                UserId = journey.UserId,
                UserFullname = journey.UserId != null ? _context.Users.Find(journey.UserId).FullName : "",
                Assesments = assesements
                //JourneyUpdates = _journeyUpdates,
                //CheckPoints = _checkpoints


            };
            return new ServiceResponse<object> { Data = details, Status = ResponseStatus.Success };
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
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };

        }
        public ServiceResponse ApproveJourney(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            journey.JourneyStatus = JourneyStatus.Approved;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };

        }



        public ServiceResponse CloseJourney(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            journey.JourneyStatus = JourneyStatus.Canceled;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }

        public ServiceResponse CompleteJourney(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            journey.JourneyStatus = JourneyStatus.Completed;
            _context.SaveChanges();
            return new ServiceResponse { Status = ResponseStatus.Success };
        }





        public ServiceResponse StopJourney(int journeyId)
        {
            var journey = _context.Journey.Find(journeyId);
            journey.JourneyStatus = JourneyStatus.Stopped;
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



        public ServiceResponse<int> AddJourneyUpdate(JourneyUpdate JourneyUpdate)
        {
            if (JourneyUpdate.Id > 0)
            {
                var item = _context.JourneyUpdate.Find(JourneyUpdate.Id);
                item.VehicleNo = JourneyUpdate.VehicleNo;
                item.DriverId = JourneyUpdate.DriverId;
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
    }
}
