using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a user role configuration
    /// </summary>
    public class MemberRoleMap : EntityTypeConfiguration<MemberRole>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<MemberRole> builder)
        {
            builder.ToTable(nameof(MemberRole));
            builder.HasOne(userRole => userRole.Role).WithMany(role => role.UserRole).HasForeignKey(userRole => userRole.RoleId).IsRequired();
            builder.HasOne(userRole => userRole.User).WithMany(user => user.UserRole).HasForeignKey(userRole => userRole.UserId).IsRequired();
        }

        #endregion
    }
}
