using Microsoft.AspNetCore.Identity;
using NakhleNakhoda.Domain.Auditable;

namespace NakhleNakhoda.Domain.Identity
{
    public class Role : IdentityRole<long>, IAuditable
    {
        public Role()
        {
        }

        public Role(string roleName) : base(roleName)
        {
        }

        public Role(string name, string description)
            : this(name)
        {
            Description = description;
        }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Gets or sets the date and time of role creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of role update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<MemberRole> UserRole { get; set; } = new HashSet<MemberRole>();
        public virtual ICollection<RoleClaim> RoleClaim { get; set; } = new HashSet<RoleClaim>();
    }
}
