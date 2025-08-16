using RapidKanban.Application.Repositories;
using RapidKanban.Infrastructure.Context;

namespace RapidKanban.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private AppDbContext Context { get; }
    public IUserstoryRepository UserstoryRepository { get; }
    public ITaskRepository TaskRepository { get; }
    public UnitOfWork(AppDbContext context, IUserstoryRepository userstoryRepository, ITaskRepository taskRepository)
    {
        Context = context;
        UserstoryRepository = userstoryRepository;
        TaskRepository = taskRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
}