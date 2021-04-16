using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketSite.Common.Models;
using RocketSite.Common.Options;

namespace RocketSite.Common.Responses
{
    public class Response4
    {
        public string EmployeeName { get; set; }
        public string EmployeeCountry { get; set; }
        public string Education { get; set; }
        public SexOption Sex { get; set; }
        public string Profession { get; set; }
        public string TrainingProgramName { get; set; }
    }
}
