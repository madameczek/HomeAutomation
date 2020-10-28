using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Models.DbModels
{
    public class IotUser : IdentityUser
    {
        // Relationships
        public ICollection<Gateway> Gateways { get; set; }
    }
}
