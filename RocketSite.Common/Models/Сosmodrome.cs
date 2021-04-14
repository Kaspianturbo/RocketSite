using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Сosmodrome
    {
        public string Name { get; set; }
        public string Timezone { get; set; }
        public Location Location { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
