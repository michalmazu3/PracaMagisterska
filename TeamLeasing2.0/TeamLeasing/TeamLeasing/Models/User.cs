using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TeamLeasing.Models
{
    public class User : IdentityUser
    {
        public virtual DeveloperUser DeveloperUser { get; set; }
        public virtual EmployeeUser EmployeeUser { get; set; }

    }
}
