using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
}