﻿using System;

namespace ImmedisHCM.Services.Models.Identity
{
    public class UserServiceModel
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
    }
}
