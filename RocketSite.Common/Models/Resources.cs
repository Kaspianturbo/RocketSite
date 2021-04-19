using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Resources
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Type")]
        public ResourceOption Type { get; set; }
        [Display(Name = "Emaunt")]
        public int Emaunt { get; set; }
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        public List<SpaceMission> SpaceMissions { get; set; }
    }
}
