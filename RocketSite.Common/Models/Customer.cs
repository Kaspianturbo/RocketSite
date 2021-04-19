using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Customer
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Total Worth")]
        public int TotalWorth { get; set; }
        public List<Cargo> Cargos { get; set; }
    }
}
