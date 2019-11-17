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
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public NotificationController(INotificationService notificationService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _notificationService = notificationService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet("{userid}")]
        [Route("getusernotifications")]
        public IActionResult GetUserNotifications(Guid userId)
        {
            return Ok(_notificationService.GetUserNotifications(userId));
        }

        [HttpPost]
        [Route("addnotification")]
        public IActionResult AddNotification(Notification notification)
        {
            return Ok(_notificationService.AddNotification(notification));
        }

        [HttpPost]
        [Route("marknotificationread")]
        public IActionResult MarkNotificationAsRead(int notificationId)
        {
            return Ok(_notificationService.MarkNotificationAsRead(notificationId));
        }

    }
}