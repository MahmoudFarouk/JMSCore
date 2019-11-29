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
    public class CheckpointService : ICheckpointService
    {
        private DatabaseContext _context;

        public CheckpointService(DatabaseContext context)
        {
            _context = context;
        }

        public ServiceResponse<List<Checkpoint>> GetCheckpoints(double startLat, double startLng, double endLat, double endLng, bool isThirdParty)
        {
            ServiceResponse<List<Checkpoint>> response = new ServiceResponse<List<Checkpoint>>();

            try
            {
                response.Data = _context.Checkpoint
                     .Where(c => c.IsThirdParty == isThirdParty).ToList()
                     .Select(c => new
                     {
                         StartDistance = LocationHandler.CalculateDistance(startLat, startLng, c.Latitude.Value, c.Longitude.Value),
                         EndDistance = LocationHandler.CalculateDistance(endLat, endLng, c.Latitude.Value, c.Longitude.Value),
                         c.Id,
                         c.Name,
                         c.Latitude,
                         c.Longitude
                     }).ToList()
                     .Where(c => (c.Latitude != startLat && c.Longitude != startLng) && (c.Latitude != endLat && c.Longitude != endLng) && (c.StartDistance <= 400 || c.EndDistance <= 400))
                     .Select(c => new Checkpoint
                     {
                         Id = c.Id,
                         Name = c.Name,
                         Latitude = c.Latitude,
                         Longitude = c.Longitude
                     }).ToList();

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

        public ServiceResponse AddCheckpoint(Checkpoint checkpoint)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.Checkpoint.Add(checkpoint);
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

        public ServiceResponse UpdateCheckpoint(Checkpoint checkpoint)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.Checkpoint.Update(checkpoint);
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

        public ServiceResponse DeleteCheckpoint(int checkpointId)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.Checkpoint.Remove(_context.Checkpoint.Find(checkpointId));
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

        public ServiceResponse<List<JourneyUpdate>> GetCheckpointsByJourneyId(int journeyId)
        {
            var result = _context.JourneyUpdate.Include(x => x.Checkpoint).Where(x => x.JourneyId == journeyId && x.CheckpointId != null).AsNoTracking().ToList();
            return new ServiceResponse<List<JourneyUpdate>> {Data=result,Status=ResponseStatus.Success };
        }


    }
}
