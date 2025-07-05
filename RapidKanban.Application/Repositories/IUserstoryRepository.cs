using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Repositories;

public interface IUserstoryRepository
{
    public Task<Userstory> CreateAsync(Userstory userstory);
    public Task<Userstory> GetById(int Id);
}