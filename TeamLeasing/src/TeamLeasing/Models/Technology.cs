using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamLeasing.Models
{
    public class Technology
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Technologia")]
        public string Name { get; set; }

        public ICollection<Developer> Developers { get; set; }
        public ICollection<Job> Jobs { get; set; }

    }
}