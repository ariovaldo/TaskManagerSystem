using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Domain.Base;
using TaskManagerSystem.Domain.Task;

namespace TaskManagerSystem.Infra.Data.Context
{
    public class TaskManagerSystemContext : DbContext
    {
        public TaskManagerSystemContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<SimpleTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity entity)
                {
                    DateTime now = DateTime.UtcNow;
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                        entity.UpdatedAt = now;

                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}