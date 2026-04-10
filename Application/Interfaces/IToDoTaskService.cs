using Application.DTO;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Application.Interfaces
{
    public interface IToDoTaskService: IBaseService
    {
        Task<ToDoTaskDTO?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(ToDoTaskDTO ToDoTaskDTO);
        Task<IReadOnlyList<ToDoTaskDTO>> GetAllAsync(GetToDoTaskRequest? getToDoTaskRequest);
        Task UpdateAsync(ToDoTaskDTO ToDoTaskDTO);
    }
}
