using Microsoft.AspNetCore.Identity;

namespace NakhleNakhoda.Domain.Identity
{
    public class MemberLogin : IdentityUserLogin<long>
    {
        public virtual Member? User { get; set; }
    }
}
