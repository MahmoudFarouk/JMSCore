using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid UserId { get; set; }
    }
}
