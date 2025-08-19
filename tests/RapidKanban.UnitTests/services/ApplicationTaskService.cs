using Moq;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Repositories;
using RapidKanban.Application.Services;
using RapidKanban.Application.Services.Tasks;
using RapidKanban.Domain.Entities;

namespace RapidKanban.UnitTests.services;

public class ApplicationTaskService
{
    public ApplicationTaskService()
    {
        
    }
    
    [Fact]
    public async Task CreateTask_Adds_To_Userstory_And_Saves()
    {
        var userstoryId = 1;
        var userstoryTitle = "Userstory Title";
        var userstoryDescription = "Userstory Description";
        var taskTitle = "Task Title";
        var taskDescription = "Task Description";
        var taskDTO = new CreateTaskDTO()
        {
            UserStoryId = userstoryId,
            Description = taskDescription,
            Title = taskTitle,
        };
        var userstory = new Userstory(userstoryTitle, userstoryDescription);
        var oldUserstoryTasksCount = userstory.Tasks.Count;
        
        var userstoryRepoMock = new Mock<IUserstoryRepository>();
        userstoryRepoMock.Setup(u => u.GetById(userstoryId)).ReturnsAsync(userstory);
        
        var uow = new Mock<IUnitOfWork>();
        uow.SetupGet(u => u.UserstoryRepository).Returns(userstoryRepoMock.Object);
        uow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

        ITaskService ts = new TaskService(uow.Object);

        var saveResult = await ts.CreateTaskAsync(taskDTO);

        Assert.True(oldUserstoryTasksCount != userstory.Tasks.Count);
        Assert.Equal(oldUserstoryTasksCount +1, userstory.Tasks.Count);
        Assert.Equal(1, saveResult);
        Assert.Contains(userstory.Tasks,
            t => ReferenceEquals(t.Userstory, userstory) && t.Title == taskTitle && t.Description == taskDescription);
        uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        uow.VerifyGet(u => u.UserstoryRepository, Times.Once);
        userstoryRepoMock.Verify(u => u.GetById(userstoryId), Times.Once);
    }
    
    [Fact]
    public async Task GetById_Gets_Task()
    {
        var userstoryTitle = "Userstory Title";
        var userstoryDescription = "Userstory Description";
        var userstory = new Userstory(userstoryTitle, userstoryDescription);
        
        var taskTitle = "Task Title";
        var taskDescription = "Task Description";
        var task = new KanbanTask(taskTitle, taskDescription, userstory);
        var taskId = 10;
        
        var taskRepoMock = new Mock<ITaskRepository>();
        taskRepoMock.Setup(u => u.GetById(taskId))
            .ReturnsAsync(task);
        
        var uow = new Mock<IUnitOfWork>();
        uow.SetupGet(u => u.TaskRepository).Returns(taskRepoMock.Object);
        
        ITaskService ts = new TaskService(uow.Object);

        var result = await ts.GetById(taskId);
        
        Assert.Equal(result.Title, task.Title);
        Assert.Equal(result.Description, task.Description);
        Assert.Equal(result.Status, task.Status);
    }
    
    [Fact]
    public async Task UpdateTask_updates_Task()
    {
        var userstoryId = 10;
        var userstoryTitle = "Userstory Title";
        var userstoryDescription = "Userstory Description";
        var userstory = new Userstory(userstoryTitle, userstoryDescription);

        var userstoryIdNew = 20;
        var userstoryTitleNew = "New Userstory Title";
        var userstoryDescriptionNew = "New Userstory Description";
        var userstoryNew = new Userstory(userstoryTitleNew, userstoryDescriptionNew);

        var taskId = 55;
        var taskTitle = "Task Title";
        var taskNewTitle = "New Task Title";
        var taskDescription = "Task Description";
        var taskNewDescription = "New Task Description";
        var taskNewStatus = Status.Developed;
        var task = new KanbanTask(taskTitle, taskDescription, userstory);

        userstory.AddTask(task);
        
        PatchTaskDTO patchTaskDTO = new PatchTaskDTO()
        {
            Title = taskNewTitle,
            Description = taskNewDescription,
            Status = taskNewStatus,
            UserStoryId = userstoryIdNew,
        };
        
        var taskRepoMock = new Mock<ITaskRepository>();
        taskRepoMock.Setup(t => t.GetById(It.IsAny<int>())).ReturnsAsync(task);
        
        var userstoryRepoMock = new Mock<IUserstoryRepository>();
        userstoryRepoMock.SetupSequence(t => t.GetById(It.IsAny<int>()))
            .ReturnsAsync(userstory)
            .ReturnsAsync(userstoryNew);
        
        var uow = new Mock<IUnitOfWork>();
        uow.SetupGet(u => u.TaskRepository).Returns(taskRepoMock.Object);
        uow.SetupGet(u => u.UserstoryRepository).Returns(userstoryRepoMock.Object);
        
        ITaskService ts = new TaskService(uow.Object);

        var result = await ts.UpdateTask(taskId, patchTaskDTO);
        
        Assert.Equal(taskNewTitle, result.Title);
        Assert.Equal(taskNewDescription, result.Description);
        Assert.Equal(taskNewStatus, result.Status);
        Assert.Contains(task, userstoryNew.Tasks);
        Assert.DoesNotContain(task,userstory.Tasks);

    }
    
    [Fact]
    public async Task DeleteTask_Deletes_Task()
    {
        var userstoryId = 10;
        var userstoryTitle = "Userstory Title";
        var userstoryDescription = "Userstory Description";
        var userstory = new Userstory(userstoryTitle, userstoryDescription);

        var taskId = 55;
        var taskTitle = "Task Title";
        var taskDescription = "Task Description";
        var task = new KanbanTask(taskTitle, taskDescription, userstory);

        userstory.AddTask(task);
        
        var taskRepoMock = new Mock<ITaskRepository>();
        taskRepoMock.Setup(t => t.GetById(It.IsAny<int>())).ReturnsAsync(task);
        
        var userstoryRepoMock = new Mock<IUserstoryRepository>();
        userstoryRepoMock.Setup(t => t.GetById(It.IsAny<int>()))
            .ReturnsAsync(userstory);
        
        var uow = new Mock<IUnitOfWork>();
        uow.SetupGet(u => u.TaskRepository).Returns(taskRepoMock.Object);
        uow.SetupGet(u => u.UserstoryRepository).Returns(userstoryRepoMock.Object);
        
        ITaskService ts = new TaskService(uow.Object);

        await ts.DeleteTask(taskId);
        
        Assert.DoesNotContain(task,userstory.Tasks);

    }
    
    

}