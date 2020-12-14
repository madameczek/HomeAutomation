using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeAutomationWebApp.Models.DbModels
{
    public class IotUser : IdentityUser
    {
        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        // Relationships
        public ICollection<Gateway> Gateways { get; set; }
    }
}
