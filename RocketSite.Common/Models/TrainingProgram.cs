using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class TrainingProgram
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Coach { get; set; }
        public int Cost { get; set; }
        public int Duration { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
