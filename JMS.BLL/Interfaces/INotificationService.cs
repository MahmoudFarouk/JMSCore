using JMS.BLL.Common;
using JMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.BLL.Interfaces
{
    public interface INotificationService
    {
        public ServiceResponse<List<Notification>> GetUserNotifications(Guid userId);

        public ServiceResponse AddNotification(Notification notification);

        public ServiceResponse MarkNotificationAsRead(int notificationId);
    }
}
