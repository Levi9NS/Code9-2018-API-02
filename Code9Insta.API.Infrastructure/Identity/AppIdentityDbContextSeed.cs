using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Code9Insta.API.Infrastructure.Identity
{
    class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@code9.com", Email = "demouser@code9.com" };
            await userManager.CreateAsync(defaultUser, "Password.1");
        }
    }
}
