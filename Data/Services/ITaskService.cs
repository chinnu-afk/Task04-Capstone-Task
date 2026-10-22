
using Task04Capstone.Models;

namespace Task04Capstone.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync(
        string? search,
        bool? isCompleted,
        int page,
        int pageSize);

    Task<TaskItem?> GetTaskByIdAsync(int id);

    Task<TaskItem> CreateTaskAsync(TaskItem task);

    Task<bool> UpdateTaskAsync(int id, TaskItem updatedTask);

    Task<bool> DeleteTaskAsync(int id);
}
