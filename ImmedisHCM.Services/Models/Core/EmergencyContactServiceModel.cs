using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class EmergencyContactServiceModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string Email { get; set; }
        public LocationServiceModel Location { get; set; }
    }
}