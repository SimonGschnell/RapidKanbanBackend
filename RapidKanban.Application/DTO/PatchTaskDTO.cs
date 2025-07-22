using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.DTO;

public class PatchTaskDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Status? Status { get; set; }
    public int? UserStoryId { get; set; }
}