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
using JMS.DAL.Common.Enums;
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
    [Route("api/request")]
    public class RequestController : ControllerBase
    {
        private IRequestService _requestService;
        private readonly AppSettings _appSettings;

        public RequestController(IRequestService requestService, IOptions<AppSettings> appSettings)
        {
            _requestService = requestService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("")]
        [Authorize(Roles = "Product Line,Dispatcher,JMC,QHSE,GBM,Operation Manager,Driver")]
        [Route("getrequests")]
        public IActionResult GetRequests()
        {
            var identityRole = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value).FirstOrDefault();

            UserRoles userRole;
            if (Enum.TryParse(identityRole.Replace(" ",""), out userRole))
            {
                var userId = Guid.Parse(User.Identity.Name);
                var result = _requestService.GetUserRequests(userId, userRole);

                if (result.Status == ResponseStatus.Success)
                    return Ok(result.Data);
            }
            return NotFound();
        }

        //[HttpPost]
        //[Route("addrequest")]
        //[Authorize(Roles = ConstRole.JMSAdmin)]
        //public IActionResult AddRequest(Request request)
        //{
        //    return Ok(_requestService.AddRequest(request));
        //}
    }



}