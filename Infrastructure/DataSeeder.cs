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
                        ToDoTaskStatus = Domain.Enums.ToDoTaskStatus.Pending
                    },
                    new ToDoTask
                    {
                        Title = "Task 2",
                        Description = "Description for Task 2",
                        ToDoTaskStatus = Domain.Enums.ToDoTaskStatus.InProgress
                    },
                    new ToDoTask
                    {
                        Title = "Task 3",
                        Description = "Description for Task 3",
                        ToDoTaskStatus = Domain.Enums.ToDoTaskStatus.Completed
                    }

                );

                await context.SaveChangesAsync();
            }
        }
    }

}
