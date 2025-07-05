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
}