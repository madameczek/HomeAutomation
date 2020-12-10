using System.ComponentModel.DataAnnotations;

namespace HomeAutomationWebApp.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string Token { get; set; }

        [Required]
        [Display(Prompt = "Enter password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Prompt = "Repeat password")]
        public string RepeatPassword { get; set; }
    }
}
