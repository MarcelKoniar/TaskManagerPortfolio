using System;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerPortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTasksController : ControllerBase
    {
        private readonly IToDoTaskService _ToDoTaskService;

        public ToDoTasksController(IToDoTaskService ToDoTaskService)
        {
            _ToDoTaskService = ToDoTaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _ToDoTaskService.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ToDoTaskDTO ToDoTask)
        {
            if (ToDoTask == null) return BadRequest();
            ToDoTask.Id = await _ToDoTaskService.AddAsync(ToDoTask);

            // Return Created pointing to the GetById endpoint. The DTO's Id may be set by the repository/EF.
            return CreatedAtAction(nameof(GetById), new { id = ToDoTask.Id }, ToDoTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetToDoTaskRequest getToDoTaskRequest)
        {
            var list = await _ToDoTaskService.GetAllAsync(getToDoTaskRequest);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ToDoTaskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch()]
        public async Task<IActionResult> Update([FromBody] ToDoTaskDTO ToDoTask)
        {
            if (ToDoTask == null) return BadRequest();

            await _ToDoTaskService.UpdateAsync(ToDoTask);
            return NoContent();
        }
    }
}
