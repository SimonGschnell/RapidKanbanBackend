using Microsoft.EntityFrameworkCore;
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

    public async Task<Userstory> GetById(int Id)
    {
        var us = await _context.Userstories.FirstOrDefaultAsync((u) => u.Id == Id);
        if (us == null)
        {
            throw new KeyNotFoundException($"Userstory with Id {Id} not found.");
        }
        return us;
    }
}