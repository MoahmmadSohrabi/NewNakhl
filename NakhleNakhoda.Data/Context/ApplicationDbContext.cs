using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Data.Mapping;
using NakhleNakhoda.Data.Models.DB;
using NakhleNakhoda.Data.Models.DB.Permissions;
using NakhleNakhoda.Data.Models.DB.User;
using NakhleNakhoda.Domain.Auditable;
using NakhleNakhoda.Domain.Identity;
using System.Reflection;

namespace NakhleNakhoda.Data
{
    public class ApplicationDbContext : IdentityDbContext<Member, Role, long, MemberClaim, MemberRole, MemberLogin, RoleClaim, MemberToken>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PardakhtHotel>()
                         .HasOne<RezervHotel>(a => a.RezervHotel)
              .WithMany(b => b.PardakhtHotels)
              .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
            //dynamically load all entity and query type configurations
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                    && (type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                IMappingConfiguration? configuration = (IMappingConfiguration?)Activator.CreateInstance(typeConfiguration);
                configuration?.ApplyConfiguration(builder);
            }

            //Config Asp.net Identity table name
            builder.Entity<Member>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<MemberRole>().ToTable("UserRole");
            builder.Entity<MemberClaim>().ToTable("UserClaim");
            builder.Entity<RoleClaim>().ToTable("RoleClaim");
            builder.Entity<MemberLogin>().ToTable("UserLogin");
            builder.Entity<MemberToken>().ToTable("UserToken");

            // This should be placed here, at the end.
            builder.AddAuditableShadowProperties();



            ///////////////////////////////////
            var cascadeFKs = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            builder.Entity<User>()
                .HasQueryFilter(u => !u.IsDelete);
            builder.Entity<Role2>()
                .HasQueryFilter(r => !r.IsDelete);



            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            SetShadowProperties();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetShadowProperties();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetShadowProperties()
        {
            ChangeTracker.SetAuditableEntityPropertyValues();
        }

        public DbSet<DargahPardakht> dargahPardakhts { get; set; }
        public DbSet<EmkanatHotel> emkanatHotels { get; set; }
        public DbSet<Hotels> hotels { get; set; }
        public DbSet<JozeyatHotel> jozeyatHotels { get; set; }
        public DbSet<Nazarat> nazarats { get; set; }
        public DbSet<PardakhtHotel> pardakhtHotels { get; set; }
        public DbSet<RezervHotel> rezervHotels { get; set; }
        public DbSet<TanzimatEmail> tanzimatEmails { get; set; }
        public DbSet<TasvirHotel> tasavirHotels { get; set; }
        public DbSet<TedadStareh> tedadStarehs { get; set; }
        public DbSet<TedadTakhtHotel> tedadTakhtHotels { get; set; }
        public DbSet<ZarfyatOtaghHotel> zarfyatOtaghHotels { get; set; }
        public DbSet<NakhleNakhoda.Data.Models.ViewModel.Jostejo> Jostejo { get; set; }

        #region User

        public DbSet<Models.DB.User.Role2> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permission { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

        #endregion


    }
}