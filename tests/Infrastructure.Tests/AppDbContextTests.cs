using Domain.EntityModels;
using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Infrastructure.Tests
{
    public class AppDbContextTests : IDisposable
    {
        private readonly ServiceProvider _provider;

        public AppDbContextTests()
        {
            var services = new ServiceCollection();

            // Register infrastructure using in-memory provider for tests
            services.AddInfrastructure();

            _provider = services.BuildServiceProvider();

            // Ensure database is created using a short-lived scope
            using var scope = _provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        [Fact]
        public void CanInsertAndQueryToDoTask()
        {
            using (var scope = _provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var task = new ToDoTask
                {
                    Title = "Test task",
                    Description = "Description",
                    Deleted = false
                };

                db.ToDoTasks.Add(task);
                db.SaveChanges();

                var tasks = db.ToDoTasks.ToList();

                Assert.Single(tasks);
                Assert.Equal("Test task", tasks[0].Title);
            }
        }

        public void Dispose()
        {
            _provider?.Dispose();
        }
    }
}
