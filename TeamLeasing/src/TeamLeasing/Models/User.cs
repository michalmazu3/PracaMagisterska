using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamLeasing.Models
{
      public class User : IdentityUser
    {
        public virtual DeveloperUser DeveloperUser { get; set; }
    }
}
