
using Microsoft.EntityFrameworkCore;
using Task04Capstone.Data;
using Task04Capstone.Models;

namespace Task04Capstone.Services;

public class TaskService : ITaskService
{
    private readonly TaskDbContext _context;

    public TaskService(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasksAsync(
        string? search,
        bool? isCompleted,
        int page,
        int pageSize)
    {
        IQueryable<TaskItem> query = _context.Tasks.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t =>
                t.Title.Contains(search) ||
                (t.Description != null &&
                 t.Description.Contains(search)));
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(t =>
                t.IsCompleted == isCompleted.Value);
        }

        return await query
            .OrderBy(t => t.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        task.Id = 0;
        task.CreatedAt = DateTime.UtcNow;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> UpdateTaskAsync(
        int id,
        TaskItem updatedTask)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsCompleted = updatedTask.IsCompleted;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
        {
            return false;
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }
}
