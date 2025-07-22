using RapidKanban.Application.DTO;
using RapidKanban.Application.Repositories;
using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Services;

public class TaskService: ITaskService
{
    public IUnitOfWork UnitOfWork { get; }
    public TaskService(IUnitOfWork uof)
    {
        UnitOfWork = uof;
    }

    public async Task<int> CreateTaskAsync(CreateTaskDTO taskDTO)
    {
        var us = await UnitOfWork.UserstoryRepository.GetById(taskDTO.UserStoryId);
        var task = new KanbanTask(taskDTO.Title, taskDTO.Description, us);
        us.AddTask(task);
        return await UnitOfWork.SaveChangesAsync();
    }

    public async Task<TaskDTO> GetById(int id)
    {
        var task = await UnitOfWork.TaskRepository.GetById(id);
        var taskDTO = new TaskDTO()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status
        };
        return taskDTO;
    }

    public async Task<TaskDTO> UpdateTask(int taskId, PatchTaskDTO patchTaskDTO)
    {
        var task = await UnitOfWork.TaskRepository.GetById(taskId);
        
        if (!string.IsNullOrWhiteSpace(patchTaskDTO.Title))
        {
            task.UpdateTitle(patchTaskDTO.Title);
        }
        
        if (!string.IsNullOrWhiteSpace(patchTaskDTO.Description))
        {
            task.UpdateDescription(patchTaskDTO.Description);
        }

        if (patchTaskDTO.Status.HasValue)
        {
            task.UpdateStatus(patchTaskDTO.Status.Value);
        }

        if (patchTaskDTO.UserStoryId.HasValue)
        {
            var oldUserStory = await UnitOfWork.UserstoryRepository.GetById(task.UserstoryId);
            var newUserStory = await UnitOfWork.UserstoryRepository.GetById(patchTaskDTO.UserStoryId.Value);
            oldUserStory.RemoveTask(task);
            newUserStory.AddTask(task);
        }

        await UnitOfWork.SaveChangesAsync();
        return new TaskDTO()
        {
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Id = task.Id
        };
    }

    public async Task DeleteTask(int taskId)
    {
        var task = await UnitOfWork.TaskRepository.GetById(taskId);
        var aggregate = await UnitOfWork.UserstoryRepository.GetById(task.UserstoryId);
        aggregate.RemoveTask(task);
        await UnitOfWork.SaveChangesAsync();
    }
}