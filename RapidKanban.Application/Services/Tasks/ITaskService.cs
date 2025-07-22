using RapidKanban.Application.DTO;

namespace RapidKanban.Application.Services;

public interface ITaskService
{
    public Task<int> CreateTaskAsync(CreateTaskDTO taskDTO);
    public Task<TaskDTO> GetById(int id);
    
    public Task<TaskDTO> UpdateTask(int taskId, PatchTaskDTO patchTaskDTO);
    public Task DeleteTask(int taskId);
}