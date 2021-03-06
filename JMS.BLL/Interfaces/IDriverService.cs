﻿using JMS.BLL.Common;
using JMS.DAL.Common.Enums;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface IDriverService
    {
        public ServiceResponse<List<User>> GetDrivers(string driverName);

        public ServiceResponse SubmitAssessment(int journeyId, int? journeyUpdateId, JourneyStatus status, List<AssessmentResult> assessmentResult);

        public ServiceResponse SubmitStatus(JourneyUpdate journeyUpdate);

        public ServiceResponse<List<AssessmentQuestion>> GetJourneyAssessment(int journeyId, bool isPreTrip, bool includeResults);

        public ServiceResponse<List<AssessmentQuestion>> GetCheckpointAssessment(int checkpoint, int journeyId, bool includeResults);
    }
}
