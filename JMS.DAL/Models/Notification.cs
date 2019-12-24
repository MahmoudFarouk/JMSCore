using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JMS.DAL.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public NotificationType? NotificationType { get; set; }
        public UserRoles? Role { get; set; }
        public int? JourneyId { get; set; }
    }
    public enum NotificationType
    {
        JourneyInfo,

    }
}
