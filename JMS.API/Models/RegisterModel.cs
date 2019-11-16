using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Models
{
    public class RegisterModel
    {
       
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public Guid? UserGroupId { get; set; }
        public Guid? UserWorkForceId { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string TrainingDetails { get; set; }
        public string GatePassStatus { get; set; }
    }
}
