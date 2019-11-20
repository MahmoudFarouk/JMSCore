﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JMS.API.Constants;
using JMS.API.Models;
using JMS.BLL.Helper;
using JMS.BLL.Interfaces;
using JMS.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JMS.API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/driver")]
    public class DriverController : ControllerBase
    {
        private IDriverService _driverService;


        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet()]
        [Authorize(Roles = "Dispatcher,JMC")]
        [Route("getdrivers/{drivername?}")]
        public IActionResult GetDrivers(string driverName = "")
        {
            return Ok(_driverService.GetDrivers(driverName));
        }


        [HttpPost]
        [Authorize(Roles = "Driver")]
        [Route("submitassessment")]
        public IActionResult SubmitAssessment(List<AssessmentResult> assessmentResult)
        {
            return Ok(_driverService.SubmitAssessment(assessmentResult));
        }


        [HttpPost]
        [Authorize(Roles = "Driver")]
        [Route("submitstatus")]
        public IActionResult SubmitStatus(JourneyUpdate status)
        {
            return Ok(_driverService.SubmitStatus(status));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getpretripassessment/{journeyid}")]
        public IActionResult GetPreJourneyAssessment(int journeyId)
        {
            return Ok(_driverService.GetJourneyAssessment(journeyId, false, false));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getposttripassessment/{journeyid}")]
        public IActionResult GetPostJourneyAssessment(int journeyId)
        {
            return Ok(_driverService.GetJourneyAssessment(journeyId, true, false));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getcheckpointassessment/{checkpointid}")]
        public IActionResult GetJourneyAssessment(int checkpointid)
        {
            return Ok(_driverService.GetCheckpointAssessment(checkpointid, false));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getpretripassessmentresult/{journeyid}")]
        public IActionResult GetPreJourneyAssessmentResult(int journeyId)
        {
            return Ok(_driverService.GetJourneyAssessment(journeyId, false, true));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getposttripassessmentresult/{journeyid}")]
        public IActionResult GetPostJourneyAssessmentResult(int journeyId)
        {
            return Ok(_driverService.GetJourneyAssessment(journeyId, true, true));
        }

        [HttpGet()]
        [Authorize(Roles = "Driver")]
        [Route("getcheckpointassessmentresult/{checkpointid}")]
        public IActionResult GetCheckpointAssessmentResult(int checkpointid)
        {
            return Ok(_driverService.GetCheckpointAssessment(checkpointid, true));
        }



    }
}