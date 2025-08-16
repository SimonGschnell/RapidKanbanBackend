using RapidKanban.Domain.Entities;

namespace RapidKanban.UnitTests.Domain;

public class UserstoryTests
{
    private Userstory CreateUserstory(string title="Test Userstory", string description="Test Userstory Description") => new Userstory(title, description);
    private KanbanTask CreateKanbanTask(Userstory userstory, string title="Test Task", string description="Test Task Description") => new KanbanTask(title, description, userstory);
    public UserstoryTests(){}

    [Fact]
    public void Constructor_Sets_Defaults()
    {
        var UserstoryTitle = "Test Userstory";
        var UserstoryDescription = "Test Userstory Description";

        var TimeBeforeUserstoryCreation = DateTime.UtcNow;
        var userstory = new Userstory(UserstoryTitle, UserstoryDescription);
        var TimeAfterUserstoryCreation = DateTime.UtcNow;

        Assert.Equal(UserstoryTitle, userstory.Title);
        Assert.Equal(DateTimeKind.Utc, userstory.CreatedAt.Kind);
        Assert.Equal(DateTimeKind.Utc, userstory.UpdatedAt.Kind);
        Assert.Equal(UserstoryDescription, userstory.Description);
        Assert.Equal(userstory.CreatedAt, userstory.UpdatedAt);
        Assert.InRange(userstory.CreatedAt, TimeBeforeUserstoryCreation, TimeAfterUserstoryCreation);
        
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData("test", null)]
    [InlineData("test", "")]
    [InlineData("test", " ")]
    [InlineData(null, "test")]
    [InlineData("", "test")]
    [InlineData(" ", "test")]
    public void Constructor_Throws_On_Invalid_Title_Description(string title, string description)
    {
        Assert.Throws<ArgumentException>(() => new Userstory(title, description));
    }

    [Fact]
    public void UpdateTitle_Updates_Values_UpdatedAt()
    {
        var userstoryNewTitle = "New Test Userstory";
        var userstory = CreateUserstory();
        var oldUserstoryUpdatedAt = userstory.UpdatedAt;
        
        userstory.UpdateTitle(userstoryNewTitle);
        
        Assert.Equal(userstoryNewTitle, userstory.Title);
        Assert.True(userstory.UpdatedAt > oldUserstoryUpdatedAt);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void UpdateTitle_Throws_On_Invalid_Input(string title)
    {
        var userstory = CreateUserstory();
        Assert.Throws<ArgumentException>(() => userstory.UpdateTitle(title));
    }
    
    [Fact]
    public void UpdateDescription_Updates_Values_UpdatedAt()
    {
        var userstoryNewDescription = "New Test Userstory Description";
        var userstory = CreateUserstory();
        var oldUserstoryUpdatedAt = userstory.UpdatedAt;
        
        userstory.UpdateDescription(userstoryNewDescription);
        
        Assert.Equal(userstoryNewDescription, userstory.Description);
        Assert.True(userstory.UpdatedAt > oldUserstoryUpdatedAt);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void UpdateDescription_Throws_On_Invalid_Input(string description)
    {
        var userstory = CreateUserstory();
        Assert.Throws<ArgumentException>(() => userstory.UpdateDescription(description));
    }

    [Fact]
    public void AddTask_Sets_Task()
    {
        var userstory = CreateUserstory();
        var task = CreateKanbanTask(userstory);
        
        userstory.AddTask(task);
        
        Assert.Contains(task, userstory.Tasks);
    }
    
    [Fact]
    public void AddTask_Throws_On_NullTask()
    {
        var userstory = CreateUserstory();
        
        Assert.Throws<ArgumentNullException>(()=>userstory.AddTask(null));
    }
    
    [Fact]
    public void RemoveTask_Removes_Task()
    {
        var userstory = CreateUserstory();
        var task = CreateKanbanTask(userstory);
        
        userstory.AddTask(task);
        userstory.RemoveTask(task);
        
        Assert.DoesNotContain(task, userstory.Tasks);
    }
    
    [Fact]
    public void RemoveTask_Throws_Removing_NonExistent_Task()
    {
        var userstory = CreateUserstory();
        var task1 = CreateKanbanTask(userstory);
        var task2 = CreateKanbanTask(userstory);
        
        userstory.AddTask(task1);
        Assert.Throws<KeyNotFoundException>(()=>userstory.RemoveTask(task2));
    }
}