using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Responses
{
    public class Response1
    {
        public string MissionName { get; set; }
        public string RocketName { get; set; }
        public string CosmodromeName { get; set; }
        public string CustomerName { get; set; }
        public int MissionCost { get; set; }
        public int Altitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
