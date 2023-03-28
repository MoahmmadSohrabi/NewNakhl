using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a user token configuration
    /// </summary>
    public class MemberTokenMap : EntityTypeConfiguration<MemberToken>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<MemberToken> builder)
        {
            builder.ToTable(nameof(MemberToken));
            builder.HasOne(userToken => userToken.User).WithMany(user => user.UserToken).HasForeignKey(userToken => userToken.UserId).IsRequired();
        }

        #endregion
    }
}
