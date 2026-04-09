using Application.DTO;
using Application.Extensions;
using Application.Interfaces;
using Domain.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IReadOnlyList<WorkTaskDTO>> GetAllAsync(GetWorkTaskRequest? request)
        {
            Expression<Func<WorkTask, bool>> filter = u => true;
            if (request != null)
            {
                if (!String.IsNullOrEmpty(request.Title))
                {
                    filter = filter.And(u => u.Title.ToUpper().Contains(request.Title.ToUpper()));
                }
                if (!String.IsNullOrEmpty(request.Description))
                {
                    filter = filter.And(u => u.Description.ToUpper().Contains(request.Description.ToUpper()));
                }
                if (request.Status != null)
                {
                    filter = filter.And(u => u.Status == request.Status);
                }
                if (request.Status != null)
                {
                    filter = filter.And(u => u.Status == request.Status);
                }

                if (request.CompletedAtFrom.HasValue)
                {
                    filter = filter.And(u => u.CreatedAt >= request.CompletedAtFrom.Value);
                }

                if (request.CompletedAtTo.HasValue)
                {
                    filter = filter.And(u => u.CreatedAt >= request.CompletedAtTo.Value);
                }
            }

            var data = await _workTaskRepository.GetWhereAsync(filter);
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
