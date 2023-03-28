using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Data.Mapping.Catalog
{
    internal class RoomCategoryMap : EntityTypeConfiguration<RoomCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<RoomCategory> builder)
        {
            builder.ToTable(nameof(RoomCategory));
            builder.HasMany(p => p.Rooms).WithOne(p => p.RoomCategory);
        }

        #endregion
    }
}