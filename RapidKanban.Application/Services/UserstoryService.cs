using RapidKanban.Application.DTO;
using RapidKanban.Application.Repositories;
using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Services;

public class UserstoryService: IUserstoryService
{
    public IUnitOfWork UnitOfWork { get; }
    public UserstoryService(IUnitOfWork uof)
    {
        UnitOfWork = uof;
    }

    public async Task<int> CreateUserstoryAsync(CreateUserstoryDTO userstoryDTO)
    {
        var userstory = new Userstory(userstoryDTO.Title, userstoryDTO.Description);
        var us= await UnitOfWork.UserstoryRepository.CreateAsync(userstory);
        await UnitOfWork.SaveChangesAsync();
        return us.Id;
    }
}