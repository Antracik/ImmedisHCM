﻿using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
