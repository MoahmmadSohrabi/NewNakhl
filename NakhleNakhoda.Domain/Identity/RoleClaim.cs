using Microsoft.AspNetCore.Identity;

namespace NakhleNakhoda.Domain.Identity
{
    public class RoleClaim : IdentityRoleClaim<long>
    {
        public virtual Role? Role { get; set; }
    }
}
