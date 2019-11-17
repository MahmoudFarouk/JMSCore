using System;
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
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public DriverController(IDriverService driverService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _driverService = driverService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{drivername}")]
        [Route("getdrivers")]
        public IActionResult GetDrivers(string driverName)
        {
            return Ok(_driverService.GetDrivers(driverName));
        }

        [HttpPost]
        [Route("submitassessment")]
        public IActionResult SubmitAssessment(List<AssessmentResult> assessmentResult)
        {
            return Ok(_driverService.SubmitAssessment(assessmentResult));
        }


        [HttpPost]
        [Route("submitassessment")]
        public IActionResult SubmitAssessment(JourneyUpdate status)
        {
            return Ok(_driverService.SubmitStatus(status));
        }

    }
}