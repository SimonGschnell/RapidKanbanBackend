namespace RapidKanban.Application.Repositories;

public interface IUnitOfWork
{
    public IUserstoryRepository UserstoryRepository { get;}
    public ITaskRepository TaskRepository { get;}
    public Task<int> SaveChangesAsync();
}