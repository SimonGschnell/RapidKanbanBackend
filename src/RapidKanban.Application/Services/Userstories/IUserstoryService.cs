using RapidKanban.Application.DTO;
using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Services;

public interface IUserstoryService
{
    public Task<int> CreateUserstoryAsync(CreateUserstoryDTO userstoryDTO);
    public Task<UserstoryDetailedDTO> GetById(int Id);
    public Task<IEnumerable<UserstoryDetailedDTO>> GetAll();
    public Task<IEnumerable<UserstoryDTO>> GetAllBasic();
    public Task<UserstoryDTO> PatchUserstory(int userstoryId, PatchUserstoryDTO patchUserstoryDto);
    public Task DeleteUserstory(int userstoryId);
}