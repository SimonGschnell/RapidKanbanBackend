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
}