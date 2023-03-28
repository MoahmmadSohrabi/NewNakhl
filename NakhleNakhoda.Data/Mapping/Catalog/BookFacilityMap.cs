using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Catalog;

namespace NakhleNakhoda.Data.Mapping.Catalog
{
    internal class BookFacilityMap : EntityTypeConfiguration<BookFacility>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<BookFacility> builder)
        {
            builder.ToTable(nameof(BookFacility));
        }

        #endregion
    }
}