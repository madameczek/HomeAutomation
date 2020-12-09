using Microsoft.AspNetCore.Identity;
using HomeAutomationWebApp.Data;
using HomeAutomationWebApp.Models.DbModels;
using HomeAutomationWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Services 
{
    public class UserManagerService : IUserManagerService
    {
        // Returns true if there is only one user with name "Administrator"
        public bool IsOnlyAdministratorExisting()
        {
            return _context.Users.Count(u => u.NormalizedUserName.StartsWith("ADMINISTRATOR")) == 1;
        }

        public bool IsEmailUnique(string email)
        {
            return _context.Users.FirstOrDefault(u => u.NormalizedEmail == email.ToUpper()) == null;
        }

        #region Dependency Injection
        private readonly AzureDbContext _context;
        public UserManagerService(AzureDbContext context)
        {
            _context = context;
        }
        #endregion
    }
}
