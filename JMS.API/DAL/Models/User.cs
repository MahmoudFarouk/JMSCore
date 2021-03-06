﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JMS.API.DAL.Models
{
    public class User : EntityBase
    {
        [MaxLength(256)]
        public string FullName { get; set; }
        public Guid? UserGroupId { get; set; }
        public Guid? UserWorkForceId { get; set; }
        [MaxLength(256)]
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string TrainingDetails { get; set; }
        public string GatePassStatus { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        public virtual UserWorkForce UserWorkForce { get; set; }
        public virtual List<UserRole> UserRoles { get; set; }

    }
}
