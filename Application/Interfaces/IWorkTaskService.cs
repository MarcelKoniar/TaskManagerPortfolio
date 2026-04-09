using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IWorkTaskService
    {
        Task<WorkTaskDTO?> GetByIdAsync(Guid id);
        Task AddAsync(WorkTaskDTO workTaskDTO);
        Task<IReadOnlyList<WorkTaskDTO>> GetAllAsync();

    }
}
