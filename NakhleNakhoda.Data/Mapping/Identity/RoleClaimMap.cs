using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a role claim configuration
    /// </summary>
    public class RoleClaimMap : EntityTypeConfiguration<RoleClaim>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.ToTable(nameof(RoleClaim));
            builder.HasOne(roleClaim => roleClaim.Role).WithMany(role => role.RoleClaim).HasForeignKey(roleClaim => roleClaim.RoleId).IsRequired();

        }

        #endregion
    }
}
