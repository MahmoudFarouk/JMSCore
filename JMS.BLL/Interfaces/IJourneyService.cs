using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using JMS.DAL.Common.Enums;

namespace JMS.BLL.Interfaces
{
    interface IJourneyService
    {
        public ServiceResponse InitiateJourney(Journey journey);

        public ServiceResponse UpdateJourney(Journey journey);

        public ServiceResponse<Journey> GetJourneyDetails(int journeyId);

        public ServiceResponse<List<Journey>> GetJourneys(DateTime date);

        public ServiceResponse AssignJourneyDriverVehicle(int journeyId, string driverId, string vehcileNo);

        public ServiceResponse ApproveJourney(int journeyId);

        public ServiceResponse CloseJourney(int journeyId);

        public ServiceResponse UpdateJourneyCheckpoint(int journeyUpdateId, JourneyStatus status);

        public ServiceResponse StopJourney(int journeyId);

        public ServiceResponse CompleteJourney(int journeyId);


    }
}
