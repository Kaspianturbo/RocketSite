using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Responses
{
    public class Response2
    {
        public string RocketName { get; set; }
        public int RocketWeight { get; set; }
        public int RocketHeight { get; set; }
        public int RocketStages { get; set; }
        public int MassToLEO { get; set; }
        public int MassToGTO { get; set; }
        public string EngineType { get; set; }
    }
}
