using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NakhleNakhoda.Domain.Media;

namespace NakhleNakhoda.Data.Mapping.Media
{
    /// <summary>
    /// Represents a picture configuration
    /// </summary>
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable(nameof(Picture));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MimeType).IsRequired().HasMaxLength(40);
            builder.Property(x => x.SeoFilename).HasMaxLength(400);
            builder.Property(x => x.TitleAttribute).HasMaxLength(400);
            builder.Property(x => x.AltAttribute).HasMaxLength(400);
        }

        #endregion

    }
}
