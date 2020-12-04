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
        [StringLength(30)]
        [Display(Prompt = "Enter your first name", Name = "first name")]
        public string FirstName { get => _firstName; set => _firstName = value?.Trim(); }

        [Required]
        [StringLength(50)]
        [Display(Prompt = "Enter your last name", Name = "last name")]
        public string LastName { get =>  _lastName; set => _lastName = value?.Trim(); }

        [Required]
        [StringLength(50)]
        [Display(Prompt = "Enter email")]
        public string Email { get => _email; set => _email = value?.Trim(); }

        [Required]
        [StringLength(30)]
        [Display(Prompt = "Enter phone number", Name = "phone number")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value?.Trim(); }

        [Required]
        [StringLength(50)]
        [Display(Prompt = "Enter password", Name = "password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Compare("Password")]
        [Display(Prompt = "Repeat password", Name = "repeat Password")]
        public string RepeatPassword { get; set; }
    }
}
