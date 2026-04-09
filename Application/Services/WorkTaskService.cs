using Application.DTO;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class WorkTaskService: IWorkTaskService
    {

        private readonly IWorkTaskRepository _workTaskRepository;

        public WorkTaskService(IWorkTaskRepository workTaskRepository)
        {
            _workTaskRepository = workTaskRepository;
        }

        public async Task<WorkTaskDTO?> GetByIdAsync(Guid id)
        {
            return await _workTaskRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(WorkTaskDTO workTaskDTO)
        {
            await _workTaskRepository.AddAsync(workTaskDTO);
        }

        public async Task<IReadOnlyList<WorkTaskDTO>> GetAllAsync()
        {
            var data = await _workTaskRepository.GetAll();

            //if (queryable == null)
            //    return Array.Empty<WorkTaskDTO>();
            return data.ToList();
        }

        //public async Task<IReadOnlyList<WorkTaskDTO?>> GetAllAsync()
        //{
        //    var data = await _workTaskRepository.GetAll();
        //    return data?.ToList();
        //}
    }
}
