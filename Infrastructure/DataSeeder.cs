using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{

    public class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.ToDoTasks.AnyAsync())
            {
                await context.ToDoTasks.AddRangeAsync(
                    new ToDoTask
                    {
                        Title = "Task 1",
                        Description = "Description for Task 1",
                        Status = Domain.Enums.Status.Pending
                    },
                    new ToDoTask
                    {
                        Title = "Task 2",
                        Description = "Description for Task 2",
                        Status = Domain.Enums.Status.InProgress
                    },
                    new ToDoTask
                    {
                        Title = "Task 3",
                        Description = "Description for Task 3",
                        Status = Domain.Enums.Status.Completed
                    }

                );

                await context.SaveChangesAsync();
            }
        }
    }

}
