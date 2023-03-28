using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Data.Mapping.Catalog
{
    internal class UserBookMap : EntityTypeConfiguration<UserBook>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<UserBook> builder)
        {
            builder.ToTable(nameof(UserBook));
            builder.HasOne(p => p.Member).WithMany(p => p.Reserves);
            builder.HasOne(p => p.Payment);
            builder.HasMany(p => p.RoomFacilities);
            builder.HasMany(p => p.BookRoomCategories);
        }

        #endregion
    }
}