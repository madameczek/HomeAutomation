using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Display(Prompt = "Enter password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Prompt = "Repeat password")]
        public string RepeatPassword { get; set; }
    }
}
