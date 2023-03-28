using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a user claim configuration
    /// </summary>
    public class MemberClaimMap : EntityTypeConfiguration<MemberClaim>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<MemberClaim> builder)
        {
            builder.ToTable(nameof(MemberClaim));
            builder.HasOne(userClaim => userClaim.User).WithMany(user => user.UserClaim).HasForeignKey(userClaim => userClaim.UserId).IsRequired();
        }

        #endregion
    }
}
