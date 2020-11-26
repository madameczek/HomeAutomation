using System.ComponentModel.DataAnnotations;

namespace HomeAutomationWebApp.Models.ViewModels
{
    public class RegistrationViewModel
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;

        [Required]
        [StringLength(12)]
        [Display(Prompt = "Enter your first name")]
        public string FirstName { get => _firstName; set => _firstName = value?.Trim(); }

        [Required]
        [Display(Prompt = "Enter your lastname")]
        public string LastName { get =>  _lastName; set => _lastName = value?.Trim(); }

        [Required]
        [Display(Prompt = "Enter email")]
        public string Email { get => _email; set => _email = value?.Trim(); }

        [Required]
        [Display(Prompt = "Enter phone number", Name = "Phone number")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value?.Trim(); }

        [Required]
        [Display(Prompt = "Enter password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Prompt = "Repeat password", Name ="Repeat Password")]
        public string RepeatPassword { get; set; }
    }
}
