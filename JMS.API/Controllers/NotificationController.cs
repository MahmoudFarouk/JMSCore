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
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;

        }

        [HttpGet()]
        [Route("getusernotifications")]
        public IActionResult GetUserNotifications()
        {
            try
            {
                var userId = Guid.Parse(User.Identity.Name);
                var model = _notificationService.GetUserNotifications(userId);
                var notifications = model.Select(x => new NotificationModel
                {
                    CreationTime = x.CreationTime,
                    Id = x.Id,
                    IsRead = x.IsRead,
                    Text = x.Text,
                    UserId = x.UserId
                }).ToList();
                return Ok(new ServiceResponse<List<NotificationModel>>
                {
                    Data = notifications,
                    Status = DAL.Common.Enums.ResponseStatus.Success
                });
            }
            catch (Exception ex)
            {
                ex.LogException();
                return Ok(new ServiceResponse
                {
                    Status = DAL.Common.Enums.ResponseStatus.ServerError
                });

            }
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