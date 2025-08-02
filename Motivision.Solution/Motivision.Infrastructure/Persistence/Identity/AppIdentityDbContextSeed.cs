using Microsoft.AspNetCore.Identity;
using Motivision.Core.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Persistence.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    FullName = "Shahd Soliamn",
                    Email = "shahd@motivision.com",
                    UserName = "shahdsoliman",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
