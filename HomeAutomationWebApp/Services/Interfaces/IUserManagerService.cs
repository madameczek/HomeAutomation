using Microsoft.AspNetCore.Identity;
using HomeAutomationWebApp.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Services.Interfaces
{
    public interface IUserManagerService
    {
        public bool IsOnlyAdministratorExisting();

        public bool IsEmailUnique(string email);

        public Task ConfirmEmailAsync(IotUser user);
    }
}
