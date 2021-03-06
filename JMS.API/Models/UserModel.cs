﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Models
{
    public class UserModel
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public Guid? UserGroupId { get; set; }
        public Guid? UserWorkForceId { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string TrainingDetails { get; set; }
        public string GatePassStatus { get; set; }
        public string Token { get; set; }
        public IList<RoleModel> Roles { get; set; }
    }
   public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
