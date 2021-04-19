using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Cargo
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Type")]
        public CargoOption Type { get; set; }
        [Display(Name = "Weight")]
        public int Weight { get; set; }
        [Display(Name = "Emaunt")]
        public int Emaunt { get; set; }
        public SpaceMission SpaceMission { get; set; }
        public Customer Customer { get; set; }
    }
}
