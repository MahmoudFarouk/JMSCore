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

    //[Authorize]
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


        [HttpGet("{startLat, startLng, endLat, endLng, isThirdParty}")]
        [Route("getcheckpoints")]
        public IActionResult GetCheckpoints(double startLat, double startLng, double endLat, double endLng, bool isThirdParty = false)
        {
            return Ok(_checkpointService.GetCheckpoints(startLat, startLng, endLat, endLng, isThirdParty));
        }

        [HttpPost]
        [Route("addcheckpoint")]
        public IActionResult AddCheckpoint(Checkpoint checkpoint)
        {
            return Ok(_checkpointService.AddCheckpoint(checkpoint));
        }

        [HttpPost]
        [Route("updatecheckpoint")]
        public IActionResult UpdateCheckpoint(Checkpoint checkpoint)
        {
            return Ok(_checkpointService.UpdateCheckpoint(checkpoint));
        }

        [HttpPost]
        [Route("deletecheckpoint")]
        public IActionResult DeleteCheckpoint(int checkpointId)
        {
            return Ok(_checkpointService.DeleteCheckpoint(checkpointId));
        }
    }



}