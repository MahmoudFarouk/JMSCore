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

        public ServiceResponse ValidateJourney(Journey journey);

        public ServiceResponse UpdateJourney(Journey journey);

        public ServiceResponse<object> GetJourneyDetails(int journeyId);

        public ServiceResponse<PageResult<Journey>> GetJourneys(DateTime? date, PagingProperties pagingProperties);

        public ServiceResponse AssignJourneyDriverVehicle(int journeyId, Guid driverId, string vehcileNo);


        public ServiceResponse UpdateJourneyCheckpoint(int journeyUpdateId, JourneyStatus status);



        public ServiceResponse<int> AssaignDreiverJourneyUpdate(JourneyUpdate JourneyUpdate);
        public Journey GetById(int id);
        public JourneyUpdate GetJourneyUpdateDriverInfo(int journeyId);
        public ServiceResponse<List<Journey>> GetUserRequests(UserRoles userRole);



        public ServiceResponse<object> JourneyCheckPoints(int journeyId);

        public ServiceResponse UpdateJourneyStatus(int journeyId, JourneyStatus status);

        public ServiceResponse AddJourneyUpdate(JourneyUpdate JourneyUpdate);

        public ServiceResponse<List<JourneyUpdate>> GetJourneyMontoring(int journeyId);

        public ServiceResponse<Journey> GetAllJourneyInfo(int journeyId);
        public ServiceResponse UpdateJourneyRiskStatus(int journeyId, bool? isNight, RiskStatus? status);
        public ServiceResponse RejectJourney(int journeyId,string commnent);

        public ServiceResponse<Journey> GetDriverCurrentJourney(Guid driverId);



    }
}
