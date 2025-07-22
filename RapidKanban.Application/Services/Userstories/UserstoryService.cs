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

    public async Task<UserstoryDetailedDTO> GetById(int Id)
    {
        var us= await UnitOfWork.UserstoryRepository.GetById(Id);
        var usDTO = new UserstoryDetailedDTO()
        {
            Id = us.Id,
            Title = us.Title,
            Description = us.Description,
            Tasks = us.Tasks.Select(t => new TaskDTO()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
            })
        };
        return usDTO;
    }
    
    public async Task<IEnumerable<UserstoryDetailedDTO>> GetAll()
    {
        var usList= await UnitOfWork.UserstoryRepository.GetAll();
        var usDTOList = usList.Select(us => new UserstoryDetailedDTO()
        {
            Id = us.Id,
            Title = us.Title,
            Description = us.Description,
            Tasks = us.Tasks.Select(t => new TaskDTO()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
            })
        });
        return usDTOList;
    }

    public async Task<IEnumerable<UserstoryDTO>> GetAllBasic()
    {
        var userstories = await UnitOfWork.UserstoryRepository.GetAll();
        var userstoriesDTO = userstories.Select(us => new UserstoryDTO()
        {
            Id = us.Id,
            Title = us.Title,
            Description = us.Description,
        });
        return userstoriesDTO;
    }

    public async Task<UserstoryDTO> PatchUserstory(int userstoryId, PatchUserstoryDTO patchUserstoryDto)
    {
        var userstory = await UnitOfWork.UserstoryRepository.GetById(userstoryId);
        if (!string.IsNullOrWhiteSpace(patchUserstoryDto.Title))
        {
            userstory.UpdateTitle(patchUserstoryDto.Title);
        }

        if (!string.IsNullOrWhiteSpace(patchUserstoryDto.Description))
        {
            userstory.UpdateDescription(patchUserstoryDto.Description);
        }
        await UnitOfWork.SaveChangesAsync();
        return new UserstoryDTO()
        {
            Id = userstory.Id,
            Title = userstory.Title,
            Description = userstory.Description,
        };
    }

    public async Task DeleteUserstory(int userstoryId)
    {
        var userstory = await UnitOfWork.UserstoryRepository.GetById(userstoryId);
        // go through domain logic to remove tasks
        foreach (var task in userstory.Tasks.ToList())
        {
            userstory.RemoveTask(task);
        }
        UnitOfWork.UserstoryRepository.Delete(userstory);
        await UnitOfWork.SaveChangesAsync();
    }
}