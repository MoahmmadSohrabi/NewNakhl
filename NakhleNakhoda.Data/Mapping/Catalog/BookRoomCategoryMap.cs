using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Data.Mapping.Catalog
{
    internal class BookRoomCategoryMap : EntityTypeConfiguration<BookRoomCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<BookRoomCategory> builder)
        {
            builder.ToTable(nameof(BookRoomCategory));
            builder.HasOne(p => p.RoomCategory);
            builder.HasOne(p => p.Room);
            builder.HasOne(p => p.UserBook).WithMany(p => p.BookRoomCategories);
        }
        #endregion
    }
}