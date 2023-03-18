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

            //modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            // Configurações personalizadas das entidades
            //modelBuilder.ApplyConfiguration(new AirfieldConfigurations());
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

                //todo: Ari - remover
                //else if (entry.Entity is IBaseEntity entity2)
                //{
                //    DateTime now = DateTime.UtcNow;
                //    if (entry.State == EntityState.Added)
                //    {
                //        entity2.CreatedAt = now;
                //        entity2.UpdatedAt = now;
                //    }
                //    else if (entry.State == EntityState.Modified)
                //        entity2.UpdatedAt = now;
                //}
            }

            return base.SaveChangesAsync(cancellationToken);
        }

    }
}