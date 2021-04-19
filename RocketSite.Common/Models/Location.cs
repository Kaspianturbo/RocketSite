using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RocketSite.Common.Models
{
    public class Location
    {
        [Display(Name = "Latitude")]
        public double Latitude { get; set; }
        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
    }
}
