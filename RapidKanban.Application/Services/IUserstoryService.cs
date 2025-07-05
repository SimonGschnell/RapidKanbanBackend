using RapidKanban.Application.DTO;

namespace RapidKanban.Application.Services;

public interface IUserstoryService
{
    public Task<int> CreateUserstoryAsync(CreateUserstoryDTO userstoryDTO);
}