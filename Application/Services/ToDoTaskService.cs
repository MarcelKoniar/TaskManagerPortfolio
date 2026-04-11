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
    public class ToDoTaskService : IToDoTaskService
    {

        private readonly IToDoTaskRepository _ToDoTaskRepository;

        public ToDoTaskService(IToDoTaskRepository ToDoTaskRepository)
        {
            _ToDoTaskRepository = ToDoTaskRepository ?? throw new ArgumentNullException(nameof(ToDoTaskRepository));
        }


        public async Task<ToDoTaskDTO?> GetByIdAsync(Guid id)
        {
            var data = await _ToDoTaskRepository.GetByIdAsync(id);
            return data !=null ? new ToDoTaskDTO { 
                Title = data.Title,
                CompletedAt = data.CompletedAt,
                Description = data.Description,
                Id = data.Id,
                ToDoTaskStatus = data.ToDoTaskStatus
            }: null;
        }

        public async Task<Guid> AddAsync(ToDoTaskDTO ToDoTaskDTO)
        {
            var newEntity = new ToDoTask
            {
                Title = ToDoTaskDTO.Title,
                Description = ToDoTaskDTO.Description,
                ToDoTaskStatus = ToDoTaskDTO.ToDoTaskStatus
            };
            var created = await _ToDoTaskRepository.AddAsync(newEntity);
            return created.Id;
        }

        public async Task<IReadOnlyList<ToDoTaskDTO>> GetAllAsync(GetToDoTaskRequest? request)
        {
            Expression<Func<ToDoTask, bool>> filter = u => true;
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
                if (request.ToDoTaskStatus != null)
                {
                    filter = filter.And(u => u.ToDoTaskStatus == request.ToDoTaskStatus);
                }
                if (request.ToDoTaskStatus != null)
                {
                    filter = filter.And(u => u.ToDoTaskStatus == request.ToDoTaskStatus);
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

            var data = await _ToDoTaskRepository.GetWhereAsync(filter);
            return data.Select(x => new ToDoTaskDTO { 
                Id = x.Id,
                ToDoTaskStatus = x.ToDoTaskStatus,
                CompletedAt = x.CompletedAt,
                Description = x.Description,
                Title = x.Title
            }).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _ToDoTaskRepository.DeleteByIdAsync(id);
        }

        public Task UpdateAsync(ToDoTaskDTO ToDoTaskDTO)
        {
            var entity = new ToDoTask
            {
                Id = ToDoTaskDTO.Id,
                Title = ToDoTaskDTO.Title,
                Description = ToDoTaskDTO.Description,
                ToDoTaskStatus = ToDoTaskDTO.ToDoTaskStatus,
                CompletedAt = ToDoTaskDTO.CompletedAt
            };
            return _ToDoTaskRepository.UpdateAsync(entity);
        }
    }
}
