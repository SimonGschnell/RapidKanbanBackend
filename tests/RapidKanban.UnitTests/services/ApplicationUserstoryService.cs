using Moq;
using RapidKanban.Application.DTO;
using RapidKanban.Application.Repositories;
using RapidKanban.Application.Services;
using RapidKanban.Domain.Entities;

namespace RapidKanban.UnitTests.services;

public class ApplicationUserstoryService
{
    public ApplicationUserstoryService()
    {
    }

    [Fact]
    public async Task CreateUserstoryAsync_Returns_UserstoryID()
    {
        var UserstoryTitle = "Userstory Title";
        var UserstoryDescription = "Userstory Description";
        var UserstoryDTO = new CreateUserstoryDTO()
        {
            Title = UserstoryTitle,
            Description = UserstoryDescription,
        };
        var UserstoryEntity = new Userstory(UserstoryTitle, UserstoryDescription);
        typeof(Userstory)?.GetProperty("Id")?.SetValue(UserstoryEntity, 10);

        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.CreateAsync(It.IsAny<Userstory>()))
            .ReturnsAsync(UserstoryEntity);

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        var result = await us.CreateUserstoryAsync(UserstoryDTO);

        Assert.Equal(10, result);
    }

    [Fact]
    public async Task GetById_Returns_Detailed_Userstory()
    {
        var UserstoryId = 10;
        var UserstoryTitle = "Userstory Title";
        var UserstoryDescription = "Userstory Description";
        var UserstoryEntity = new Userstory(UserstoryTitle, UserstoryDescription);
        typeof(Userstory)?.GetProperty("Id")?.SetValue(UserstoryEntity, UserstoryId);

        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.GetById(It.IsAny<int>()))
            .ReturnsAsync(UserstoryEntity);

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        var result = await us.GetById(UserstoryId);

        Assert.IsAssignableFrom<IEnumerable<TaskDTO>>(result.Tasks);
    }
    
    [Fact]
    public async Task GetAll_Returns_A_UserstoryDetailed_Collection()
    {
        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.GetAll())
            .ReturnsAsync(new List<Userstory>());

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        var result = await us.GetAll();

        Assert.IsAssignableFrom<IEnumerable<UserstoryDetailedDTO>>(result);
    }
    
    [Fact]
    public async Task GetAllBasic_Returns_A_Userstory_Collection()
    {
        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.GetAll())
            .ReturnsAsync(new List<Userstory>());

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        var result = await us.GetAllBasic();

        Assert.IsAssignableFrom<IEnumerable<UserstoryDTO>>(result);
    }
    
    [Theory]
    [InlineData("new title","new description")]
    [InlineData("","new description")]
    [InlineData("new title","")]
    [InlineData(null,"new description")]
    [InlineData("new title",null)]
    [InlineData(" ","new description")]
    [InlineData("new title"," ")]
    public async Task PatchUserstory_Updates_Title_Or_Description(string title, string description)
    {
        var UserstoryId = 10;
        var UserstoryTitle = "Old Userstory Title";
        var UserstoryDescription = "Old Userstory Description";
        var UserstoryEntity = new Userstory(UserstoryTitle, UserstoryDescription);
        typeof(Userstory)?.GetProperty("Id")?.SetValue(UserstoryEntity, UserstoryId);

        var UserstoryPatch = new PatchUserstoryDTO()
        {
            Title = title,
            Description = description,
        };
        
        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.GetById(UserstoryId))
            .ReturnsAsync(UserstoryEntity);

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        var result = await us.PatchUserstory(UserstoryId,UserstoryPatch);

        var ExpectedTitle = string.IsNullOrWhiteSpace(title) ? UserstoryTitle : title;
        var ExpectedDescription = string.IsNullOrWhiteSpace(description) ? UserstoryDescription : description;
        Assert.Equal(ExpectedTitle,UserstoryEntity.Title);
        Assert.Equal(ExpectedDescription,UserstoryEntity.Description);
    }
    
    [Fact]
    public async Task DeleteUserstory_Removes_Tasks()
    {
        var UserstoryId = 10;
        var UserstoryTitle = "Old Userstory Title";
        var UserstoryDescription = "Old Userstory Description";
        var UserstoryEntity = new Userstory(UserstoryTitle, UserstoryDescription);
        typeof(Userstory)?.GetProperty("Id")?.SetValue(UserstoryEntity, UserstoryId);

        var AddedTask = new KanbanTask("KanbanTask Title", "KanbanTask Description", UserstoryEntity);
        UserstoryEntity.AddTask(AddedTask);

        var UserrepositoryMock = new Mock<IUserstoryRepository>();
        UserrepositoryMock.Setup(u => u.GetById(UserstoryId))
            .ReturnsAsync(UserstoryEntity);

        var UnitOfWorkMock = new Mock<IUnitOfWork>();
        UnitOfWorkMock.SetupGet(u => u.UserstoryRepository).Returns(UserrepositoryMock.Object);

        IUserstoryService us = new UserstoryService(UnitOfWorkMock.Object);
        await us.DeleteUserstory(UserstoryId);
        
        Assert.Empty(UserstoryEntity.Tasks);
    }
    
    
}