﻿using System.ComponentModel.DataAnnotations;

namespace HomeAutomationWebApp.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Prompt = "Enter email")]
        public string Email { get; set; }
        [Required]
        [Display(Prompt = "Enter password")]
        public string Password { get; set; }
    }
}
