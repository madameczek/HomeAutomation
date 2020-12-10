using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Services
{
    public class WebAppEmailService : IWebAppEmailService
    {
        public Task SendEmailConfirmation(string confirmationLink, IotUser user)
        {
            var result = _emailService.SendAsync(
                user.Email,
                //"marek@adameczek.pl",
                $"{user.FirstName}, confirm your registration",
                $"<h3>Kliknij link, by potwierdzić rejestrację do serwisu 'Home Automation'</h3><br />" +
                $"<a href=\"{confirmationLink}\">Potwierdź adres email</a><br />" +
                $"Zignoruj tę wiadomość, jeśli nie donowywałeś(aś) rejestracji.",
                true);
            if (result.IsCompletedSuccessfully)
            {
                _logger.LogDebug("Account activation email sent to {Email}.", user.Email);
            }
            return Task.CompletedTask;
        }

        public Task SendResetPasswordConfirmation(string confirmationLink, IotUser user)
        {
            _emailService.SendAsync(
                user.Email,
                $"{user.FirstName}, zlecono zmianę hasła",
                "<h3>Otrzymujesz ten email, bo uruchomiono opcję resetowania hasła do serwisu 'Home Automation'</h3><br />" +
                $"Kliknij <a href=\"{confirmationLink}\">link</a>, by potwierdzić operację<br />" +
                "Nastąpi przekierowanie do formularza zmiany hasła.<br />" +
                "Zignoruj tę wiadomość, jeśli nie zlecełeś(aś) operacji.",
                true);
            return Task.CompletedTask;
        }

        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        public WebAppEmailService(IEmailService emailService, ILoggerFactory loggerFactory)
        {
            _emailService = emailService;
            _logger = loggerFactory.CreateLogger("Email Service");
        }
    }
}
