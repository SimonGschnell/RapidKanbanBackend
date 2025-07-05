using RapidKanban.Application.Repositories;
using RapidKanban.Infrastructure.Context;

namespace RapidKanban.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private AppDbContext Context { get; }
    public IUserstoryRepository UserstoryRepository { get; }
    public UnitOfWork(AppDbContext context, IUserstoryRepository userstoryRepository)
    {
        Context = context;
        UserstoryRepository = userstoryRepository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Context.SaveChangesAsync();
    }
}