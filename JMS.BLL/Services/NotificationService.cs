using JMS.BLL.Common;
using JMS.BLL.Interfaces;
using JMS.DAL.Common.Enums;
using JMS.DAL.Context;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JMS.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private DatabaseContext _context;

        public NotificationService(DatabaseContext context)
        {
            _context = context;
        }

        public ServiceResponse<List<Notification>> GetUserNotifications(Guid userId)
        {
            ServiceResponse<List<Notification>> response = new ServiceResponse<List<Notification>>();

            try
            {
                response.Data = _context.Notification.Where(n => n.UserId == userId).ToList();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;
        }

        public ServiceResponse AddNotification(Notification notification)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.Notification.Add(notification);
                _context.SaveChanges();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;
        }

        public ServiceResponse MarkNotificationAsRead(int notificationId)
        {
            ServiceResponse response = new ServiceResponse();

            try
            {
                _context.Notification.Find(notificationId).IsRead = true;
                _context.SaveChanges();

                response.Status = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = ResponseStatus.ServerError;

                ExceptionLogger.LogException(ex);
            }

            return response;
        }
    }
}
