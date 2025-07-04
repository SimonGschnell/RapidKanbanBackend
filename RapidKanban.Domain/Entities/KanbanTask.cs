namespace RapidKanban.Domain.Entities;

public class KanbanTask
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public int UserstoryId { get; private set; }
    public virtual Userstory Userstory { get; private set; }

    private KanbanTask() { }
    public KanbanTask(string title, string description, Userstory userstory)
    {
        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Title or Description cannot be empty.");
        Title = title;
        Description = description;
        Status = Status.New;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
        Userstory = userstory ?? throw new ArgumentNullException(nameof(userstory));
        UserstoryId = userstory.Id;
    }
    
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.");
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateStatus(Status status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
    
}