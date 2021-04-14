using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Education { get; set; }
        public SexOption Sex { get; set; }
        public int Profession { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}
