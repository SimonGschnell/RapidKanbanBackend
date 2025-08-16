using RapidKanban.Domain.Entities;

namespace RapidKanban.UnitTests.Domain;

public class KanbanTaskTests
{
    private readonly Userstory Userstory = new Userstory("Test Userstory", "Test Userstory Description");
    private KanbanTask CreateKanbanTask(string title="Test Task", string description="Test Task Description") => new KanbanTask(title,description, Userstory);

    public KanbanTaskTests()
    {
        
    }
    
    [Fact]
    public void Constructor_Sets_Defaults_Relationships()
    {
        // Act
        // TODO: create KanbanTask with non-empty title/description and the userstory
        var taskTitle = "Test Task";
        var taskDescription = "Test Task Description";
        
        var taskCreatedAtLowerBound = DateTime.UtcNow;
        var task = new KanbanTask(taskTitle, taskDescription, Userstory);
        var taskCreatedAtUpperBound = DateTime.UtcNow;

        // Assert
        // TODO: Title, Description match inputs
        // TODO: Status == Status.New
        // TODO: Userstory and UserstoryId are set consistently
        // TODO: CreatedAt within [before, after]; UpdatedAt == CreatedAt
        
        //! We use Assert.Equal to check if two references are equal by value
        Assert.Equal(taskTitle, task.Title);
        Assert.Equal(taskDescription, task.Description);
        Assert.Equal(Status.New, task.Status);
        
        //! We use Assert.Same to check whether two references are the same instance
        Assert.Same(Userstory, task.Userstory);
        Assert.Equal(Userstory.Id, task.UserstoryId);
        Assert.Equal(task.Userstory.Id, task.UserstoryId);
        
        
        Assert.Equal(DateTimeKind.Utc, task.CreatedAt.Kind);
        Assert.Equal(DateTimeKind.Utc, task.UpdatedAt.Kind);
        //! We use Assert.InRange to check whether a value is between to bounds
        Assert.InRange(task.CreatedAt, taskCreatedAtLowerBound, taskCreatedAtUpperBound);

        Assert.Equal(task.CreatedAt, task.UpdatedAt);

    }

    [Fact]
    public void Constructor_Throws_On_Null_Userstory()
    {
        Assert.Throws<ArgumentNullException>(()=> new KanbanTask("Test Task", "Test Task Description", null!));
    }
    
    [Theory]
    [InlineData("","")]
    [InlineData("test","")]
    [InlineData("","test")]
    [InlineData(null,null)]
    [InlineData("",null)]
    [InlineData(null,"")]
    [InlineData(null,"test")]
    [InlineData("test",null)]
    [InlineData("test"," ")]
    [InlineData(" ","test")]
    public void Constructor_throws_on_empty_Title_Description(string taskTitle, string taskDescription)
    {
        //Act
        Assert.Throws<ArgumentException>(() => new KanbanTask(taskTitle, taskDescription, Userstory));
    }
    
    [Fact]
    public void UpdateTitle_Changes_Title_And_Bumps_UpdatedAt()
    {
        var taskTitleNew = "New Test Task";

        var task = CreateKanbanTask();
        var oldUpdatedAt = task.UpdatedAt;
        
        task.UpdateTitle(taskTitleNew);
        
        Assert.Equal(taskTitleNew, task.Title);
        Assert.True(task.UpdatedAt > oldUpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void UpdateTitle_Throws_On_Invalid_Input(string NewTitle)
    {
        KanbanTask kanbanTask = CreateKanbanTask();
        Assert.Throws<ArgumentException>(()=>kanbanTask.UpdateTitle(NewTitle));
    }

    [Fact]
    public void UpdateDescription_Changes_Description_And_Bumps_UpdatedAt()
    {
        var taskNewDescription = "New Test Task Description";

        var task = CreateKanbanTask();
        var oldUpdatedAt = task.UpdatedAt;
        
        task.UpdateDescription(taskNewDescription);
        
        Assert.Equal(taskNewDescription, task.Description);
        Assert.True(task.UpdatedAt > oldUpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void UpdateDescription_Throws_On_Invalid_Input(string NewDescription)
    {
        KanbanTask kanbanTask = CreateKanbanTask();
        Assert.Throws<ArgumentException>(()=>kanbanTask.UpdateDescription(NewDescription));
    }
    
    [Fact]
    public void UpdateStatus_Changes_Status_And_Bumps_UpdatedAt()
    {
        var taskNewStatus = Status.InDevelopment;

        var task = CreateKanbanTask();
        var oldTaskUpdatedAt = task.UpdatedAt;
        var oldTaskStatus = task.Status;
        
        task.UpdateStatus(taskNewStatus);
        
        Assert.Equal(taskNewStatus, task.Status);
        Assert.NotEqual(oldTaskStatus, task.Status);
        Assert.True(task.UpdatedAt > oldTaskUpdatedAt);
    }
}