using Microsoft.AspNetCore.Identity;
using NakhleNakhoda.Common;
using NakhleNakhoda.Domain.Auditable;
using NakhleNakhoda.Domain.Catalog;
using System.ComponentModel.DataAnnotations.Schema;

namespace NakhleNakhoda.Domain.Identity
{
    public class Member : IdentityUser<long>, IAuditable
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Otp { get; set; } = "";
        public DateTime OtpExpireAt { get; set; }
        public bool Active { get; set; }
        public string LastIpAddress { get; set; } = "";
        public DateTime LastLoginDateUtc { get; set; }

        [NotMapped]
        public string DisplayName
        {
            get
            {
                var displayName = $"{FirstName} {LastName}";
                return string.IsNullOrWhiteSpace(displayName) ? "" + PhoneNumber : displayName;
            }
        }

        public DateTime? BirthDate { get; set; }
        public string SocialNumber { get; set; } = "";
        public int GenderId { get; set; }
        public string CardNumber { get; set; } = "";
        public string AccountNumber { get; set; } = "";
        public string ShebaNumber { get; set; } = "";

        public string IdentifierNumber { get; set; } = "";

        public string Token { get; set; } = "";
        /// <summary>   
        /// Gets or sets a value indicating number of failed login attempts (wrong password)
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// Gets or sets the date and time until which a customer cannot login (locked out)
        /// </summary>
        public DateTime? CannotLoginUntilDateUtc { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether the customer has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time of user creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of user update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the picture identifier
        /// </summary>
        public long PictureId { get; set; }

        public Gender Gender { get; set; }
        public virtual ICollection<UserBook> Reserves { get; set; } = new List<UserBook>();
        public virtual ICollection<MemberToken> UserToken { get; set; } = new List<MemberToken>();
        public virtual ICollection<MemberRole> UserRole { get; set; } = new List<MemberRole>();
        public virtual ICollection<MemberLogin> UserLogin { get; set; } = new List<MemberLogin>();
        public virtual ICollection<MemberClaim> UserClaim { get; set; } = new List<MemberClaim>();
    }
}