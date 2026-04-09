using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IWorkTaskRepository
    {

        Task<WorkTaskDTO?> GetByIdAsync(Guid id);
        Task AddAsync(WorkTaskDTO workTaskDTO);
        Task<IQueryable<WorkTaskDTO?>> GetAll();
        
    }
}
