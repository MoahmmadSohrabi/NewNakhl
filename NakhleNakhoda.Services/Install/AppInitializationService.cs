using Microsoft.AspNetCore.Identity;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Services.Install
{
    public class AppInitializationService : IAppInitializationService
    {
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AppInitializationService(
            UserManager<Member> userManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            if (!(await _roleManager.RoleExistsAsync(RoleNames.Developer)))
            {
                var roleResult = await _roleManager.CreateAsync(new Role { Name = RoleNames.Developer, Description = RoleNames.Developer_Description });
                if (roleResult.Succeeded)
                {
                    await _roleManager.CreateAsync(new Role { Name = RoleNames.Admin, Description = RoleNames.Admin_Description });
                    await _roleManager.CreateAsync(new Role { Name = RoleNames.Manager, Description = RoleNames.Manager_Description });
                    await _roleManager.CreateAsync(new Role { Name = RoleNames.Member, Description = RoleNames.Member_Description });

                    var user = new Member
                    {
                        UserName = "mahdi",
                        Email = "mahdi.shojaei68@gmail.com",
                        FirstName = "مهدی",
                        LastName = "شجاعی",
                        PhoneNumber = "09377963337",
                        Active = true,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };
                    var userResult = await _userManager.CreateAsync(user, "12qw!@QW");

                    if (userResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, RoleNames.Developer);
                        await _userManager.AddToRoleAsync(user, RoleNames.Admin);
                        await _userManager.AddToRoleAsync(user, RoleNames.Manager);
                        await _userManager.AddToRoleAsync(user, RoleNames.Member);
                    }
                }
            }
        }
    }
}
