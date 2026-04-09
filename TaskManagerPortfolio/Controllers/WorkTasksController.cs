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
            workTask.Id = await _workTaskService.AddAsync(workTask);

            // Return Created pointing to the GetById endpoint. The DTO's Id may be set by the repository/EF.
            return CreatedAtAction(nameof(GetById), new { id = workTask.Id }, workTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetWorkTaskRequest getWorkTaskRequest)
        {
            var list = await _workTaskService.GetAllAsync(getWorkTaskRequest);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _workTaskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch()]
        public async Task<IActionResult> Update([FromBody] WorkTaskDTO workTask)
        {
            if (workTask == null) return BadRequest();

            await _workTaskService.UpdateAsync(workTask);
            return NoContent();
        }
    }
}
