using RapidKanban.Application.DTO;

namespace RapidKanban.Application.Services;

public interface ITaskService
{
    public Task<int> CreateTaskAsync(CreateTaskDTO taskDTO);
}