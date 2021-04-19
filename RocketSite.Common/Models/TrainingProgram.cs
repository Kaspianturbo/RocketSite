using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class TrainingProgram
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Area")]
        public string Area { get; set; }
        [Display(Name = "Coach")]
        public string Coach { get; set; }
        [Display(Name = "Cost")]
        public int Cost { get; set; }
        [Display(Name = "Duration")]
        public int Duration { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
