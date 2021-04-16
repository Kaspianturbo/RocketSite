using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketSite.Common.Models;
using RocketSite.Common.Options;

namespace RocketSite.Common.Responses
{
    public class Response3
    {
        public string CosmodromeName { get; set; }
        public string Timezone { get; set; }
        public Location Location { get; set; }
        public string MissionName { get; set; }
        public StatusOption StatusOption { get; set; }
    }
}
