namespace RapidKanban.CQRS;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{

    Task Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand<TResult>
{

    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}