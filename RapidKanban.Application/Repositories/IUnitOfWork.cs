namespace RapidKanban.Application.Repositories;

public interface IUnitOfWork
{
    public IUserstoryRepository UserstoryRepository { get;}
    public Task<int> SaveChangesAsync();
}