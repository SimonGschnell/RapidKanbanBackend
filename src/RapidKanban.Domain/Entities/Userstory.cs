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
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Invalid title of description for the userstory");
        }
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void AddTask(KanbanTask kanbanTask)
    {
        if (kanbanTask is null)
        {
            throw new ArgumentNullException("task is null");
        }
        _tasks.Add(kanbanTask);
    }

    public void RemoveTask(KanbanTask task)
    {
        if (_tasks.Contains(task))
        {
            _tasks.Remove(task);
        }
        else
        {
            throw new KeyNotFoundException($"userstory {Id} does not contain task {task.Id}");
        }
    }
    
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("title has invalid value");
        }
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("description value is invalid");
        }
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}