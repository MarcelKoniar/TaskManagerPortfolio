using System;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerPortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkTasksController : ControllerBase
    {
        private readonly IWorkTaskService _workTaskService;

        public WorkTasksController(IWorkTaskService workTaskService)
        {
            _workTaskService = workTaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _workTaskService.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkTaskDTO workTask)
        {
            if (workTask == null) return BadRequest();
            await _workTaskService.AddAsync(workTask);

            // Return Created pointing to the GetById endpoint. The DTO's Id may be set by the repository/EF.
            return CreatedAtAction(nameof(GetById), new { id = workTask.Id }, workTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _workTaskService.GetAllAsync();
            return Ok(list);
        }
    }
}
