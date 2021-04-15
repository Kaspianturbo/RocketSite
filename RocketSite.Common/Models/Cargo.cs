using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Cargo
    {
        public string Name { get; set; }
        public CargoOption Type { get; set; }
        public int Weight { get; set; }
        public int Emaunt { get; set; }
        public Customer Customer { get; set; }
    }
}
