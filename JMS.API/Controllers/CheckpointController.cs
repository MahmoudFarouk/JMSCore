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
using JMS.BLL.Common;
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
    [Route("api/checkpoint")]
    public class CheckpointController : ControllerBase
    {
        private ICheckpointService _checkpointService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CheckpointController(ICheckpointService checkpointService, IOptions<AppSettings> appSettings)
        {
            _checkpointService = checkpointService;
            _appSettings = appSettings.Value;
        }


        [HttpGet("{fromLat, fromLng, toLat, toLng, isThirdParty}")]
        [Authorize(Roles = "Product Line,Dispatcher,JMC")]
        [Route("getcheckpoints")]
        public IActionResult GetCheckpoints(double fromLat, double fromLng, double toLat, double toLng, bool isThirdParty = false)
        {
            return Ok(_checkpointService.GetCheckpoints(fromLat, fromLng, toLat, toLng, isThirdParty));
        }

        [HttpPost]
        [Route("addcheckpoint")]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        public IActionResult AddCheckpoint(Checkpoint checkpoint)
        {
            return Ok(_checkpointService.AddCheckpoint(checkpoint));
        }

        [HttpPost]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("updatecheckpoint")]
        public IActionResult UpdateCheckpoint(Checkpoint checkpoint)
        {
            return Ok(_checkpointService.UpdateCheckpoint(checkpoint));
        }

        [HttpPost]
        [Authorize(Roles = ConstRole.JMSAdmin)]
        [Route("deletecheckpoint")]
        public IActionResult DeleteCheckpoint(int checkpointId)
        {
            return Ok(_checkpointService.DeleteCheckpoint(checkpointId));
        }

        [HttpGet]
        [Authorize(Roles = ConstRole.Driver)]
        [Route("getCheckpointsByJourneyId")]
        public IActionResult GetCheckpointsByJourneyId(int journeyId)
        {
            try
            {
                return Ok(_checkpointService.GetCheckpointsByJourneyId(journeyId));
            }catch(Exception ex)
            {
                return Ok(new ServiceResponse {Status=DAL.Common.Enums.ResponseStatus.ServerError,Exception=ex.ToString() });
            }
        }
    }



}