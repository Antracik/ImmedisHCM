using System;
using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class CountryServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public IList<CityServiceModel> Cities { get; set; }
    }
}