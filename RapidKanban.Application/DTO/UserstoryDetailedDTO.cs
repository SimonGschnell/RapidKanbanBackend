namespace RapidKanban.Application.DTO;

public class UserstoryDetailedDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public IEnumerable<TaskDTO>? Tasks { get; init; }
}