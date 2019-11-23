using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using JMS.DAL.Common.Enums;
using JMS.BLL.Models;

namespace JMS.BLL.Interfaces
{
    public interface IJourneyService
    {
        public ServiceResponse InitiateJourney(Journey journey);

        public ServiceResponse UpdateJourney(Journey journey);

        public ServiceResponse<object> GetJourneyDetails(int journeyId);

        public ServiceResponse<PageResult<Journey>> GetJourneys(DateTime? date, PagingProperties pagingProperties);

        public ServiceResponse AssignJourneyDriverVehicle(int journeyId, Guid driverId, string vehcileNo);

        public ServiceResponse ApproveJourney(int journeyId);

        public ServiceResponse CloseJourney(int journeyId);

        public ServiceResponse UpdateJourneyCheckpoint(int journeyUpdateId, JourneyStatus status);

        public ServiceResponse StopJourney(int journeyId);

        public ServiceResponse CompleteJourney(int journeyId);

        public ServiceResponse<int> AddJourneyUpdate(JourneyUpdate JourneyUpdate);
        public Journey GetById(int id);
        public JourneyUpdate GetJourneyUpdateDriverInfo(int journeyId);

        public ServiceResponse<object> JourneyCheckPoints(int journeyId);


    }
}
