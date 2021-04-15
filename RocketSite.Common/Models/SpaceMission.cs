using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class SpaceMission
    {
        public string Name { get; set; }
        public StatusOption Status { get; set; }
        public int Cost { get; set; }
        public int Altitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Rocket Rocket { get; set; }
        public List<Resources> Resources { get; set; }
        public List<Cargo> CargoList { get; set; }
    }
}
