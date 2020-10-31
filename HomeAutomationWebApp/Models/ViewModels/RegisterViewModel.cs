using System.ComponentModel.DataAnnotations;

namespace HomeAutomationWebApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Prompt = "Enter your first name")]
        public string Name { get; set; }

        [Required]
        [Display(Prompt = "Enter your lastname")]
        public string Surname { get; set; }

        [Required]
        [Display(Prompt = "Enter email")]
        public string Email { get; set; }

        [Required]
        [Display(Prompt = "Enter phone number", Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Prompt = "Enter password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Prompt = "Repeat password", Name ="Repeat Password")]
        public string RepeatPassword { get; set; }
    }
}
