using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class CityServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CountryServiceModel Country { get; set; }
    }
}