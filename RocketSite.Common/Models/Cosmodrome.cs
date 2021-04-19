using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Cosmodrome
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Timezone")]
        public string Timezone { get; set; }
        [Display(Name = "Location")]
        public Location Location { get; set; }
        public List<SpaceMission> SpaceMissions { get; set; }
    }
}
