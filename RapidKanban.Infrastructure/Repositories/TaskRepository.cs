using Microsoft.EntityFrameworkCore;
using RapidKanban.Application.Repositories;
using RapidKanban.Domain.Entities;
using RapidKanban.Infrastructure.Context;

namespace RapidKanban.Infrastructure.Repositories;

public class TaskRepository: ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<KanbanTask> CreateAsync(KanbanTask task)
    {
        await _context.Tasks.AddAsync(task);
        return task;
    }

    public async Task<KanbanTask> GetById(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        if (task is null)
        {
            throw new KeyNotFoundException($"Task with id {id} not found");
        }

        return task;
    }

}