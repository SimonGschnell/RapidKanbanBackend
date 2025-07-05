using RapidKanban.Application.DTO;
using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Services;

public interface IUserstoryService
{
    public Task<int> CreateUserstoryAsync(CreateUserstoryDTO userstoryDTO);
    public Task<Userstory> GetById(int Id);
}