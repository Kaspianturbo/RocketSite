using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Rocket
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Diameter { get; set; }
        public int Cost { get; set; }
        public int Stages { get; set; }
        public int MassToLEO { get; set; }
        public int MassToGTO { get; set; }
        public string EngineType { get; set; }
    }
}
