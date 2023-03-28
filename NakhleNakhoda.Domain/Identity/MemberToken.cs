using Microsoft.AspNetCore.Identity;

namespace NakhleNakhoda.Domain.Identity
{
    public class MemberToken : IdentityUserToken<long>
    {
        public virtual Member? User { get; set; }
    }
}
