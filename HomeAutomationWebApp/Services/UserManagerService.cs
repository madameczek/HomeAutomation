using HomeAutomationWebApp.Data;
using HomeAutomationWebApp.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using HomeAutomationWebApp.Models.DbModels;

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

        public async Task ConfirmEmailAsync(IotUser user)
        {
            _context.Users.FirstOrDefault(u => u.Email == user.Email).EmailConfirmed = true;
            await _context.SaveChangesAsync();
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
