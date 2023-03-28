using Microsoft.AspNetCore.Identity;

namespace NakhleNakhoda.Domain.Identity
{
    public class MemberRole : IdentityUserRole<long>
    {

        public virtual Member? User { get; set; }

        public virtual Role? Role { get; set; }
    }
}
