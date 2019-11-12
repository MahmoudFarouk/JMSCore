using JMS.API.Repositories;
using System.Net;
using System.Net.Http;
using static JMS.API.Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using JMS.API.Common;
using JMS.API.Models;

namespace JMS.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class JourneyController : ControllerBase
    {
        private readonly JourneyRepository journeyRepo;

        public JourneyController()
        {
            journeyRepo = new JourneyRepository();
        }

        [HttpGet]
        public ServiceResponse<List<Checkpoint>> GetCheckpoints(string lat = "", string lng = "")
        {
            return journeyRepo.GetCheckpoints(lat, lng);
        }

    }
}
