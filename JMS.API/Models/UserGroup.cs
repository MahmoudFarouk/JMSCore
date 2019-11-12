﻿using System;
using System.Collections.Generic;

namespace JMS.API.Models
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
