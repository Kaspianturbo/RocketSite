using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class SpaceMission
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Status")]
        public StatusOption Status { get; set; }
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        [Display(Name = "Altitude")]
        public int Altitude { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Rocket")]
        public Rocket Rocket { get; set; }
        [Display(Name = "Cosmodrome")]
        public Cosmodrome Cosmodrome { get; set; }
        public List<Resources> Resources { get; set; }
        public List<Cargo> Cargoes { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
