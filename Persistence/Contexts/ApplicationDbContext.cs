using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkeletonApi.Domain.Common.Abstracts;
using SkeletonApi.Domain.Common.Interfaces;
using SkeletonApi.Domain.Entities;
using SkeletonApi.Domain.Entities.Tsdb;
using System.Data;

namespace SkeletonApi.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
    UserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        private readonly IDomainEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IDomainEventDispatcher dispatcher = null)
            : base(options)
        {
            _dispatcher = dispatcher;
        }

        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Operator> Operators => Set<Operator>();
        public DbSet<Zone> Zones => Set<Zone>();
        public DbSet<Types> Types => Set<Types>();
        public DbSet<DeviceData> DeviceData => Set<DeviceData>();
        public DbSet<SettingTask> SettingTasks => Set<SettingTask>();
        public DbSet<ActivityUser> ActivityUsers => Set<ActivityUser>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId);
            });


                modelBuilder.Entity<Role>()
               .HasMany(e => e.Permissions)
               .WithOne(e => e.Role)
               .HasForeignKey(e => e.RoleId)
               .IsRequired(false);

            modelBuilder.Entity<ActivityUser>(
            eb =>
            {
                eb.Property(b => b.Id).HasColumnName("id").HasColumnType("uuid");
                eb.Property(b => b.UserName).HasColumnName("username").HasColumnType("text");
                eb.Property(b => b.LogType).HasColumnName("logtype").HasColumnType("text");
                eb.Property(b => b.DateTime).HasColumnName("datetime").HasColumnType("timestamp");
            });

            modelBuilder.Entity<Account>(
            eb =>
            {
                eb.Property(b => b.Username).HasColumnName("username").HasColumnType("text");
                eb.Property(b => b.PhotoURL).HasColumnName("photo_url").HasColumnType("text");
            });

            modelBuilder.Entity<Operator>()
              .HasMany(e => e.SettingTasks)
              .WithOne(e => e.Operator)
              .HasForeignKey(e => e.OperatorId)
              .IsRequired(false);

            modelBuilder.Entity<Zone>()
              .HasMany(e => e.Operators)
              .WithOne(e => e.Zone)
              .HasForeignKey(e => e.ZoneId)
              .IsRequired(false);

            modelBuilder.Entity<Zone>()
              .HasMany(e => e.Types)
              .WithOne(e => e.Zone)
              .HasForeignKey(e => e.ZoneId)
              .IsRequired(false);

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        public IDbConnection Connection { get; }
    }
}