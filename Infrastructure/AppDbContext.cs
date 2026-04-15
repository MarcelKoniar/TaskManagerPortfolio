

using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        { 
        }

        public DbSet<ToDoTask> ToDoTasks => Set<ToDoTask>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoTask>(entity =>
            {
                // Primary key
                entity.HasKey(e => e.Id);

                // Index
                entity.HasIndex(e => e.Deleted);

                // Properties
                entity.Property(e => e.Deleted)
                      .IsRequired();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //var currUser = await UserProvider.CurrentUser(); TODO: Implement user provider to get current user information         
            var userName = "Test User";

            var currentDate = DateTime.Now;

            foreach (var entry in base.ChangeTracker.Entries())
            {
                if (entry.Entity.GetType().IsSubclassOf(typeof(BaseModel)))
                {
                    var model = (BaseModel)entry.Entity;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            model.CreatedAt = currentDate;
                            model.CreatedBy = userName;                           
                            goto case EntityState.Modified;
                        case EntityState.Modified:
                            model.UpdatedAt = currentDate;
                            model.UpdatedBy = userName;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
        }
    }

}
