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
    public class CheckpointController : ControllerBase
    {
        private ICheckpointService _checkpointService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CheckpointController(ICheckpointService checkpointService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _checkpointService = checkpointService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
    }
}