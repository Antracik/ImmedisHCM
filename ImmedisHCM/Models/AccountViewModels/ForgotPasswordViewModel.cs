using System.ComponentModel.DataAnnotations;

namespace ImmedisHCM.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
