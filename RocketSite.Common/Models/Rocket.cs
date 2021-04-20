using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Rocket
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Version")]
        public string Version { get; set; }
        [Display(Name = "Weight")]
        public int Weight { get; set; }
        [Display(Name = "Height")]
        public int Height { get; set; }
        [Display(Name = "Diameter")]
        public int Diameter { get; set; }
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        [Display(Name = "Stages")]
        public int Stages { get; set; }
        [Display(Name = "Mass to LEO")]
        public int MassToLEO { get; set; }
        [Display(Name = "Mass to GTO")]
        public int MassToGTO { get; set; }
        [Display(Name = "Engine type")]
        public string EngineType { get; set; }
        public List<SpaceMission> SpaceMissions { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}
