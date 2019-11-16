using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Location;
using System.Linq;

namespace JMS.BLL.Services
{
    public class CheckpointService : ICheckpointService
    {
        private DatabaseContext _context;

        public CheckpointService(DatabaseContext context)
        {
            _context = context;
        }

        public ServiceResponse<List<Checkpoint>> GetCheckpoints(string startLat, string startLng, string endLat, string endLng, bool isThirdParty)
        {
            ServiceResponse<List<Checkpoint>> response = new ServiceResponse<List<Checkpoint>>();

            try
            {
                var sCoOrd = new GeoCoordinate(double.Parse(startLat), double.Parse(startLat));
                var eCoOrd = new GeoCoordinate(double.Parse(endLat), double.Parse(endLng));

                var x = _context.Checkpoint
                    .Where(c => isThirdParty ? true : !c.IsThirdParty)
                    .Select(c => new
                    {
                        StartDistance = sCoOrd.GetDistanceTo(new GeoCoordinate(double.Parse(c.Lat), double.Parse(c.Lng))) / 1000,
                        EndDistance = eCoOrd.GetDistanceTo(new GeoCoordinate(double.Parse(c.Lat), double.Parse(c.Lng))) / 1000,
                        c.Id,
                        c.Name,
                        c.Lat,
                        c.Lng
                    })
                    .Where(c => c.StartDistance <= 100 && c.EndDistance <= 100)
                    .Select(c => new Checkpoint
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Lat = c.Lat,
                        Lng = c.Lng
                    });

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
