using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Domain.EntityModels;

namespace Application.Services
{
    public class WorkTaskService : IWorkTaskService
    {

        private readonly IWorkTaskRepository _workTaskRepository;

        public WorkTaskService(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository ?? throw new ArgumentNullException(nameof(workTaskRepository));
        }


        public async Task<WorkTaskDTO?> GetByIdAsync(Guid id)
        {
            var data = await _workTaskRepository.GetByIdAsync(id);
            return new WorkTaskDTO { 
                Title = data.Title,
                CompletedAt = data.CompletedAt,
                Description = data.Description,
                Id = data.Id,
                Status = data.Status
            };
        }

        public async Task<Guid> AddAsync(WorkTaskDTO workTaskDTO)
        {
            var newEntity = new WorkTask
            {
                Title = workTaskDTO.Title,
                Description = workTaskDTO.Description,
                Status = workTaskDTO.Status
            };
            var created = await _workTaskRepository.AddAsync(newEntity);
            return created.Id;
        }

        public async Task<IReadOnlyList<WorkTaskDTO>> GetAllAsync()
        {
            var data = await _workTaskRepository.GetAllAsync();
            return data.Select(x => new WorkTaskDTO { 
                Id = x.Id,
                Status = x.Status,
                CompletedAt = x.CompletedAt,
                Description = x.Description,
                Title = x.Title
            }).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _workTaskRepository.DeleteByIdAsync(id);
        }

        public Task UpdateAsync(WorkTaskDTO workTaskDTO)
        {
            var entity = new WorkTask
            {
                Id = workTaskDTO.Id,
                Title = workTaskDTO.Title,
                Description = workTaskDTO.Description,
                Status = workTaskDTO.Status,
                CompletedAt = workTaskDTO.CompletedAt
            };
            return _workTaskRepository.UpdateAsync(entity);
        }
    }
}
