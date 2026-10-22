
using Microsoft.AspNetCore.Mvc;
using Task04Capstone.Models;
using Task04Capstone.Services;

namespace Task04Capstone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // GET: api/tasks?search=report&isCompleted=false&page=1&pageSize=10
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetTasks(
        [FromQuery] string? search,
        [FromQuery] bool? isCompleted,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        if (page < 1)
        {
            return BadRequest(new { message = "Page must be at least 1." });
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(new
            {
                message = "Page size must be between 1 and 100."
            });
        }

        var tasks = await _taskService.GetTasksAsync(
            search, isCompleted, page, pageSize);

        return Ok(tasks);
    }

    // GET: api/tasks/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetTask(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);

        if (task is null)
        {
            return NotFound(new { message = "Task not found." });
        }

        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask(
        [FromBody] TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new
            {
                message = "Task title is required."
            });
        }

        task.Title = task.Title.Trim();

        var createdTask = await _taskService.CreateTaskAsync(task);

        return CreatedAtAction(
            nameof(GetTask),
            new { id = createdTask.Id },
            createdTask);
    }

    // PUT: api/tasks/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTask(
        int id,
        [FromBody] TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new
            {
                message = "Task title is required."
            });
        }

        task.Title = task.Title.Trim();

        var updated = await _taskService.UpdateTaskAsync(id, task);

        if (!updated)
        {
            return NotFound(new { message = "Task not found." });
        }

        return NoContent();
    }

    // DELETE: api/tasks/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var deleted = await _taskService.DeleteTaskAsync(id);

        if (!deleted)
        {
            return NotFound(new { message = "Task not found." });
        }

        return NoContent();
    }
}
