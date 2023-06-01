using System.Reflection;
using JubilantBroccoli.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JubilantBroccoli.Infrastructure.Core.Base
{
    public class ApplicationDbContext : DbContextBase
    {
        public SaveChangesResult SaveChangesResult { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }


        private void DbSaveChanges()
        {
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

            foreach (var entity in addedEntities)
            {
                if (entity.Entity is not IAuditable)
                    return;

                var createdAt = entity.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var updatedAt = entity.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;
                
                if (!DateTime.TryParse(createdAt.ToString(), out var parsedCreatedAt))
                    entity.Property(nameof(IAuditable.CreatedAt)).CurrentValue = _defaultDatetime;
                if (updatedAt != null && !DateTime.TryParse(updatedAt.ToString(), out var parsedUpdatedAt))
                    entity.Property(nameof(IAuditable.CreatedAt)).CurrentValue = _defaultDatetime;

            }

            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
        }
    }
}