using Application.DTO;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IWorkTaskService: IBaseService
    {
        Task<WorkTaskDTO?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(WorkTaskDTO workTaskDTO);
        Task<IReadOnlyList<WorkTaskDTO>> GetAllAsync();
        Task UpdateAsync(WorkTaskDTO workTaskDTO);
    }
}
