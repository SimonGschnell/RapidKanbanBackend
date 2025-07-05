using RapidKanban.Application.Repositories;
using RapidKanban.Domain.Entities;
using RapidKanban.Infrastructure.Context;

namespace RapidKanban.Infrastructure.Repositories;

public class UserstoryRepository: IUserstoryRepository
{
    private readonly AppDbContext _context;
    public UserstoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Userstory> CreateAsync(Userstory userstory)
    {
        await _context.Userstories.AddAsync(userstory);
        return userstory;
    }
}