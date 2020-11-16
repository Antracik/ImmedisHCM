using System;
using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models
{
    public class CityViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "City")]
        public string Name { get; set; }
    }
}