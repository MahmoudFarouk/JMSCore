using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Models
{
    public class JourneyDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTruckTransport { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string FromDestination { get; set; }
        public double FromLat { get; set; }
        public double FromLng { get; set; }
        public string ToDistination { get; set; }
        public double ToLat { get; set; }
        public double ToLng { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? CargoWeight { get; set; }
        public Level CargoPriority { get; set; }
        public Level CargoSeverity { get; set; }
        public string CargoType { get; set; }
        public Guid UserId { get; set; }
        public string UserFullname { get; set; }
        public bool IsThirdParty { get; set; }
        public IList<JourneyUpdateModel> JourneyUpdates { get; set; }
        public IList<CheckPointModel> CheckPoints { get; set; }
    }
    public class JourneyUpdateModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? JourneyId { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string VehicleNo { get; set; }
        public string DriverId { get; set; }
        public string Drivername { get; set; }
        public bool IsJourneyCheckpoint { get; set; }
        public int? CheckpointId { get; set; }
        public Level RiskLevel { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDriverStatus { get; set; }
        public bool IsAlert { get; set; }
        public string StatusMessage { get; set; }
        public IList<AssessmentQuestionModel> AssessmentQuestions { get; set; }


    }
    public class CheckPointModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsThirdParty { get; set; }
    }
    public class AssessmentQuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public QuestionCategory Category { get; set; }
        public int? Weight { get; set; }
        public bool IsThirdParty { get; set; }
        public int? CheckpointId { get; set; }
        public IList<AssessmentResultModel> AssessmentResults { get; set; }
    }
    public class AssessmentResultModel
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public bool IsYes { get; set; }
        public string Comment { get; set; }
        public Guid SubmittedBy { get; set; }
        public string SubmittedByname { get; set; }
        public int? VehicleNo { get; set; }
        public int? JourneyUpdateId { get; set; }
    }
}
