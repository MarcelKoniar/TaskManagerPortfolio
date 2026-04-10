using Application.DTO;
using Application.Interfaces;
using Domain.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class ToDoTaskRepository: BaseRepository<ToDoTask>, IToDoTaskRepository
    {

        //private readonly AppDbContext _dbContext;

        //public ToDoTaskRepository(AppDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //    _dbContext.Database.EnsureCreated();
        //}

        //public async Task<ToDoTaskDTO?> GetByIdAsync(Guid id)
        //{
        //    var user = await _dbContext.ToDoTasks.FirstOrDefaultAsync(x => x.Id == id);

        //    return user == null ? null : new ToDoTaskDTO
        //    {
        //        Id = user.Id,
        //        Title = user.Title,
        //        Description = user.Description,
        //        Status = user.Status,
        //        CompletedAt = user.CompletedAt
        //    };
        //}

        //public async Task AddAsync(ToDoTaskDTO user)
        //{
        //    await _dbContext.ToDoTasks.AddAsync(new ToDoTask
        //    {   Id = Guid.NewGuid(),             
        //        Title = user.Title,
        //        Description = user.Description, 
        //        Status = user.Status,
        //        CompletedAt = user.CompletedAt                    
        //    });

        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task<IQueryable<ToDoTaskDTO?>> GetAll()
        //{
        //    return _dbContext.ToDoTasks.Where(x => !x.Deleted).Select(x => new ToDoTaskDTO
        //    {
        //        Id = x.Id,
        //        CompletedAt = x.CompletedAt,
        //        Description = x.Description,
        //        Status = x.Status,
        //        Title = x.Title
        //    });
        //}
        public ToDoTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        
    }
}
