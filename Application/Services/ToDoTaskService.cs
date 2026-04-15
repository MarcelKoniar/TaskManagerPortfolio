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

        private readonly IToDoTaskRepository _toDoTaskRepository;

        public ToDoTaskService(IToDoTaskRepository toDoTaskRepository)
        {
            _toDoTaskRepository = toDoTaskRepository ?? throw new ArgumentNullException(nameof(toDoTaskRepository));
        }


        public async Task<ToDoTaskDTO?> GetByIdAsync(Guid id)
        {
            var data = await _toDoTaskRepository.GetByIdAsync(id);
            return data !=null ? new ToDoTaskDTO { 
                Title = data.Title,
                CompletedAt = data.CompletedAt,
                Description = data.Description,
                Id = data.Id,
                ToDoTaskStatus = data.ToDoTaskStatus
            }: null;
        }

        public async Task<Guid> AddAsync(ToDoTaskDTO toDoTaskDTO)
        {
            var newEntity = new ToDoTask
            {
                Title = toDoTaskDTO.Title,
                Description = toDoTaskDTO.Description,
                ToDoTaskStatus = toDoTaskDTO?.ToDoTaskStatus != null ? toDoTaskDTO.ToDoTaskStatus : Domain.Enums.ToDoTaskStatus.Pending,
            };
            var created = await _toDoTaskRepository.AddAsync(newEntity);
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

            var data = await _toDoTaskRepository.GetWhereAsync(filter);
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
            await _toDoTaskRepository.DeleteByIdAsync(id);
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
            return _toDoTaskRepository.UpdateAsync(entity);
        }
    }
}
