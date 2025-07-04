namespace RapidKanban.Domain.Entities;

public class Userstory
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private readonly List<KanbanTask> _tasks = new List<KanbanTask>();
    public virtual IReadOnlyCollection<KanbanTask> Tasks => _tasks.AsReadOnly();

    private Userstory() { }
    public Userstory(string title, string description)
    {
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void AddTask(KanbanTask kanbanTask)
    {
        _tasks.Add(kanbanTask);
    }
    
    public void UpdateTitle(string title)
    {
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}