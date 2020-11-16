using System;

namespace ImmedisHCM.Data.Entities
{
    public class EmergencyContact
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string HomePhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Location Location { get; set; }
    }
}