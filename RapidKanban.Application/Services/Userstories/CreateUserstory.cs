using RapidKanban.Application.DTO;
using RapidKanban.Application.Repositories;
using RapidKanban.CQRS;
using RapidKanban.Domain.Entities;

namespace RapidKanban.Application.Services;

public sealed record CreateUserstoryCommand(string title, string description): ICommand<UserstoryDTO>;

internal sealed class CreateUserstoryHandler : ICommandHandler<CreateUserstoryCommand, UserstoryDTO>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserstoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<UserstoryDTO> Handle(CreateUserstoryCommand command, CancellationToken cancellationToken)
    {
        var userstory = new Userstory(title: command.title, description: command.description);
        var createdUserstory = await _unitOfWork.UserstoryRepository.CreateAsync(userstory);
        await _unitOfWork.SaveChangesAsync();
        return new UserstoryDTO()
        {
            Id = createdUserstory.Id,
            Title = createdUserstory.Title,
            Description = createdUserstory.Description
        };
    }
}