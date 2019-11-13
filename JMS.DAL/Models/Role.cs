using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMS.DAL.Models
{
    public class Role:EntityBase
    {
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        public virtual List<UserRole> UsersRole { get; set; }
    }
}
