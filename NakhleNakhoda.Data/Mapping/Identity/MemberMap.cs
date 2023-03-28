using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Data.Mapping.Identity
{
    /// <summary>
    /// Represents a user configuration
    /// </summary>
    public class MemberMap : EntityTypeConfiguration<Member>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(nameof(Member));
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
        }

        #endregion
    }
}
