using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Resources
    {
        public string Name { get; set; }
        public ResourceOption Type { get; set; }
        public int Emaunt { get; set; }
        public int Cost { get; set; }
        public List<SpaceMission> SpaceMissions { get; set; }
    }
}
