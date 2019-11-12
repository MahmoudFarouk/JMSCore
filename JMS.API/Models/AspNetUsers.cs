using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AssessmentResult = new HashSet<AssessmentResult>();
            DriverStatusUpdate = new HashSet<DriverStatusUpdate>();
            Journey = new HashSet<Journey>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? UserGroupId { get; set; }
        public int? UserWorkForceId { get; set; }
        public string LicenseNo { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        public string TrainingDetails { get; set; }
        public string GatePassStatus { get; set; }

        public virtual UserGroup UserGroup { get; set; }
        public virtual UserWorkForce UserWorkForce { get; set; }
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AssessmentResult> AssessmentResult { get; set; }
        public virtual ICollection<DriverStatusUpdate> DriverStatusUpdate { get; set; }
        public virtual ICollection<Journey> Journey { get; set; }
    }
}
