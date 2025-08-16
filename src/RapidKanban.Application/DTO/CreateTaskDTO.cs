namespace RapidKanban.Application.DTO;

public class CreateTaskDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserStoryId { get; set; }
}