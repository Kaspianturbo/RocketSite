using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Responses
{
    public class Response1
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public int Cost { get; set; }
        public int Altitude { get; set; }
        public Rocket Rocket { get; set; }
        public Cosmodrome Cosmodrome { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
