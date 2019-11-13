using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JMS.API.DAL.Models
{
    public class UserGroup:EntityBase
    {
        
        [MaxLength(256)]
        [Required]
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
