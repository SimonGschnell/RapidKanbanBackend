using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Repositories;

public interface ITaskRepository
{
    public Task<KanbanTask> CreateAsync(KanbanTask task);
    
    public Task<KanbanTask> GetById(int id);
}