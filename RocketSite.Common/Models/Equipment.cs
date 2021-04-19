using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Equipment
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Producer")]
        public string Producer { get; set; }
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        public Rocket Rocket { get; set; }
    }
}
