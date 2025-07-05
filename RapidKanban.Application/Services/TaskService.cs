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
}