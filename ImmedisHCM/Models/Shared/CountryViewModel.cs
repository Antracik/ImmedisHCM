using System;
using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models
{
    public class CountryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        [Display(Name = "Country")]
        public string CombinedName => $"{Name} ({ShortName})";
    }
}