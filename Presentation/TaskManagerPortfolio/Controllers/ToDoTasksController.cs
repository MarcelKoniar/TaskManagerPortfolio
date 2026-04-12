using Application.DTO;
using Application.Interfaces;
using Domain.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TaskManagerPortfolio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoTasksController : ControllerBase
    {
        private readonly IToDoTaskService _toDoTaskService;

        public ToDoTasksController(IToDoTaskService toDoTaskService)
        {
            _toDoTaskService = toDoTaskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var dto = await _toDoTaskService.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ToDoTaskDTO toDoTask)
        {
            if (toDoTask == null) return BadRequest();
            toDoTask.Id = await _toDoTaskService.AddAsync(toDoTask);

            // Return Created pointing to the GetById endpoint. The DTO's Id may be set by the repository/EF.
            return CreatedAtAction(nameof(GetById), new { id = toDoTask.Id }, toDoTask);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetToDoTaskRequest getToDoTaskRequest)
        {
            var list = await _toDoTaskService.GetAllAsync(getToDoTaskRequest);
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _toDoTaskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPatch()]
        public async Task<IActionResult> Update([FromBody] ToDoTaskDTO toDoTask)
        {
            if (toDoTask == null) return BadRequest();

            await _toDoTaskService.UpdateAsync(toDoTask);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ToDoTaskDTO toDoTask)
        {
            if (toDoTask == null) return BadRequest();

            await _toDoTaskService.UpdateAsync(toDoTask);
            return NoContent();
        }
    }
}
