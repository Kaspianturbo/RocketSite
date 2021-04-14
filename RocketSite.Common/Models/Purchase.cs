using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Purchase
    {
        public string Name { get; set; }
        public Resources Resources { get; set; }
        public string Cost { get; set; }
        public Employee Employee { get; set; }
    }
}
