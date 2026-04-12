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
        public ToDoTaskRepository(AppDbContext dbContext) : base(dbContext)
        {
        }        
    }
}
