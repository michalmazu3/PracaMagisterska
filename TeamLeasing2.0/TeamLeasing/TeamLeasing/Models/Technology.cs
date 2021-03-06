﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamLeasing.Models
{
    public class Technology
    {

        public int Id { get; set; }
        [Display(Name = "Technologia")]
        public string Name { get; set; }

        public ICollection<DeveloperUser> DeveloperUsers { get; set; }

        
        public ICollection<Job> Jobs { get; set; }

    }
}