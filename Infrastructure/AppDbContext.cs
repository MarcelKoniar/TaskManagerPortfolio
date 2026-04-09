

using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { 
        }

        public DbSet<WorkTask> WorkTasks => Set<WorkTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WorkTask>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Index
                entity.HasIndex(e => e.Deleted);

                // Properties
                entity.Property(e => e.Deleted)
                      .IsRequired();

                //entity.Property(e => e.CreatedBy)
                //      .HasMaxLength(256);

                //entity.Property(e => e.UpdatedBy)
                //      .HasMaxLength(256);
            });

            //modelBuilder.ApplyConfigurationsFromAssembly(
            //    typeof(AppDbContext).Assembly
            //);
        }
    }

}
