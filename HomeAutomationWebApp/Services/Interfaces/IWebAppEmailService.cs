using HomeAutomationWebApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Services.Interfaces
{
    public interface IWebAppEmailService
    {
        public Task SendEmailConfirmation(string confirmationLink, IotUser user);

        public Task SendResetPasswordConfirmation(string confirmationLink, IotUser user);
    }
}
