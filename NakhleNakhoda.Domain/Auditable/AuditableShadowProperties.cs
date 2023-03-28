using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NakhleNakhoda.Domain.Auditable
{
    public static class AuditableShadowProperties
    {
        public static readonly Func<object, DateTime> EFPropertyCreatedOnUtc =
                                        entity => EF.Property<DateTime>(entity, CreatedOnUtc!);
        public static readonly string CreatedOnUtc = nameof(CreatedOnUtc);

        public static readonly Func<object, DateTime> EFPropertyUpdatedOnUtc =
                                        entity => EF.Property<DateTime>(entity, UpdatedOnUtc!);
        public static readonly string UpdatedOnUtc = nameof(UpdatedOnUtc);


        public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model
                                                   .GetEntityTypes()
                                                   .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)))
            {

                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime>(CreatedOnUtc);
                modelBuilder.Entity(entityType.ClrType)
                            .Property<DateTime>(UpdatedOnUtc);
            }
        }

        public static void SetAuditableEntityPropertyValues(
            this ChangeTracker changeTracker)
        {
            var now = DateTime.UtcNow;

            var UpdatedEntries = changeTracker.Entries<IAuditable>()
                                               .Where(x => x.State == EntityState.Modified);
            foreach (var UpdatedEntry in UpdatedEntries)
            {
                UpdatedEntry.Property(UpdatedOnUtc).CurrentValue = now;
            }

            var addedEntries = changeTracker.Entries<IAuditable>()
                                            .Where(x => x.State == EntityState.Added);
            foreach (var addedEntry in addedEntries)
            {
                addedEntry.Property(CreatedOnUtc).CurrentValue = now;
                addedEntry.Property(UpdatedOnUtc).CurrentValue = now;
            }
        }

    }
}