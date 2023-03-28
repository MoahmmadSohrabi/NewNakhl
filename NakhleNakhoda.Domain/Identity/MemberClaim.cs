using Microsoft.AspNetCore.Identity;

namespace NakhleNakhoda.Domain.Identity
{
    public class MemberClaim : IdentityUserClaim<long>
    {
        public virtual Member? User { get; set; }
    }
}
