using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NakhleNakhoda.Domain.Identity;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace NakhleNakhoda.Services.Identity
{
    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<Member, Role>
    {
        public ApplicationClaimsPrincipalFactory(
            UserManager<Member> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options
            ) : base(userManager, roleManager, options)
        { }

        public override async Task<ClaimsPrincipal> CreateAsync(Member user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var
                principal = await base
                    .CreateAsync(
                        user); // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically + `Options.ClaimsIdentity.UserIdClaimType -> userId` & `Options.ClaimsIdentity.UserNameClaimType -> userName`
            AddCustomClaims(user, principal);
            return principal;
        }

        private static void AddCustomClaims(Member user, IPrincipal principal)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            ((ClaimsIdentity?)principal.Identity)?.AddClaims(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture),
                ClaimValueTypes.Integer),
            new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
            new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new Claim(ClaimTypes.MobilePhone, "0" + user.PhoneNumber ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            });
        }
    }
}