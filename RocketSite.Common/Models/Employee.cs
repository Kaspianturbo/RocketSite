using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Employee
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Education")]
        public string Education { get; set; }
        [Display(Name = "Sex")]
        public SexOption Sex { get; set; }
        [Display(Name = "Profession")]
        public string Profession { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
        public SpaceMission SpaceMission { get; set; }
    }
}
