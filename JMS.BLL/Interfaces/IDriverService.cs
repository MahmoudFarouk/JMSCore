using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface IDriverService
    {
        public ServiceResponse<List<User>> GetDrivers(string driverName);

        public ServiceResponse SubmitAssessment(List<AssessmentResult> assessmentResult);

        public ServiceResponse SubmitStatus(JourneyUpdate journeyUpdate);



    }
}
