using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Commons;
using TodoApp.Application.TaskItems.Commands.CreateTaskItem;
using TodoApp.Application.TaskItems.Commands.DeleteTaskItem;
using TodoApp.Application.TaskItems.Commands.UpdateTaskItem;
using TodoApp.Application.TaskItems.Queries.GetAllTaskItems;
using TodoApp.Application.TaskItems.Queries.GetTaskItemById;

namespace TodoApp.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly IBroker _broker;

        public TaskItemsController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskItems(CancellationToken cancellationToken)
        {
            var query = new GetAllTaskItemsQuery();
            var result = await _broker.QueryAsync(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(long id, CancellationToken cancellationToken)
        {
            var query = new GetTaskItemByIdQuery(id);
            var result = await _broker.QueryAsync(query, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem(CreateTaskItemCommand command, CancellationToken cancellationToken)
        {
            var result = await _broker.CommandAsync(command, cancellationToken);
            return CreatedAtAction(nameof(GetTaskItemById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTaskItem(long id, UpdateTaskItemCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var result = await _broker.CommandAsync(command, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(long id, CancellationToken cancellationToken)
        {
            var command = new DeleteTaskItemCommand(id);
            var result = await _broker.CommandAsync(command, cancellationToken);
            if (!result.Data)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
