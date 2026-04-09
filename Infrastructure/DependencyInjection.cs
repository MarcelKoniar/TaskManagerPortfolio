using Application.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {


            // In-memory SQLite requires an open connection to persist across contexts
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            // Database
            services.AddDbContext<AppDbContext>(options =>
            {
                // Use InMemory for now (tests / dev)
                options.UseSqlite(connection);

                // 👉 switch later to SQL Server / PostgreSQL
                // options.UseSqlServer(
                //     configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IWorkTaskRepository, WorkTaskRepository>();

            return services;
        }
    }
}
