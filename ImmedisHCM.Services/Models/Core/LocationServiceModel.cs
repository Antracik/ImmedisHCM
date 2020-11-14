using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class LocationServiceModel
    {
        public Guid Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public CityServiceModel City { get; set; }
        public string PostalCode { get; set; }
    }
}