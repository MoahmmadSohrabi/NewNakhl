using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a user login configuration
    /// </summary>
    public class MemberLoginMap : EntityTypeConfiguration<MemberLogin>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<MemberLogin> builder)
        {
            builder.ToTable(nameof(MemberLogin));
            builder.HasOne(userLogin => userLogin.User).WithMany(user => user.UserLogin).HasForeignKey(userLogin => userLogin.UserId).IsRequired();
        }

        #endregion
    }
}
