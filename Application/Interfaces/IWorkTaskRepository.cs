using Application.DTO;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IWorkTaskRepository : IBaseRepository<WorkTask>
    {

        //Task<WorkTaskDTO?> GetByIdAsync(Guid id);
        //Task AddAsync(WorkTaskDTO workTaskDTO);
        //Task<IQueryable<WorkTaskDTO?>> GetAll();

    }
}
