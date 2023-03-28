using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Data.Mapping.Catalog
{
    internal class RoomMap : EntityTypeConfiguration<Room>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(nameof(Room));
            builder.HasOne(p => p.RoomCategory).WithMany(p => p.Rooms).OnDelete(DeleteBehavior.NoAction);
        }

        #endregion
    }
}