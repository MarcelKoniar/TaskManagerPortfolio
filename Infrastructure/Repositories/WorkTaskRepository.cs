using Application.DTO;
using Application.Interfaces;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class WorkTaskRepository: IWorkTaskRepository
    {

        private readonly AppDbContext _dbContext;

        public WorkTaskRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public async Task<WorkTaskDTO?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.WorkTasks.FirstOrDefaultAsync(x => x.Id == id);

            return user == null ? null : new WorkTaskDTO
            {
                Id = user.Id,
                Title = user.Title,
                Description = user.Description,
                Status = user.Status,
                CompletedAt = user.CompletedAt
            };
        }

        public async Task AddAsync(WorkTaskDTO user)
        {
            await _dbContext.WorkTasks.AddAsync(new WorkTask
            {   Id = Guid.NewGuid(),             
                Title = user.Title,
                Description = user.Description, 
                Status = user.Status,
                CompletedAt = user.CompletedAt                    
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IQueryable<WorkTaskDTO?>> GetAll()
        {
            return _dbContext.WorkTasks.Where(x => !x.Deleted).Select(x => new WorkTaskDTO
            {
                Id = x.Id,
                CompletedAt = x.CompletedAt,
                Description = x.Description,
                Status = x.Status,
                Title = x.Title
            });
        }

    }
}
