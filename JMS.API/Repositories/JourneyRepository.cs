using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JMS.API.Common;
using JMS.API.Models;
using static JMS.API.Common.Enums;

namespace JMS.API.Repositories
{

    public sealed class JourneyRepository 
    {
        private JMSDBContext dbContext;


        public JourneyRepository()
        {
            dbContext = new JMSDBContext();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }


        public ServiceResponse<List<Checkpoint>> GetCheckpoints(string lat, string lng)
        {
            ServiceResponse<List<Checkpoint>> response = new ServiceResponse<List<Checkpoint>>();

            try
            {
                response.Data = dbContext.Checkpoint.ToList(); ;
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

        public ServiceResponse AddJourney(Journey journey)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {

                dbContext.Journey.Add(journey);
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
