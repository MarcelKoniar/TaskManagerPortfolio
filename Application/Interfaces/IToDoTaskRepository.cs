using Application.DTO;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IToDoTaskRepository : IBaseRepository<ToDoTask>
    {

        //Task<ToDoTaskDTO?> GetByIdAsync(Guid id);
        //Task AddAsync(ToDoTaskDTO ToDoTaskDTO);
        //Task<IQueryable<ToDoTaskDTO?>> GetAll();

    }
}
