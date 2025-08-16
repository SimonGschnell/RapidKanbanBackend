namespace RapidKanban.CQRS;

public interface ICommandDispatcher
{
    public Task Send(ICommand command, CancellationToken cancellationToken=default);
    public Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken=default);

}

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task Send(ICommand command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType());
        var handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for type {handlerType}");
        }

        await (Task)handlerType.GetMethod("Handle")!.Invoke(handler, new object[] { command, cancellationToken })!;
    }

    public async Task<TResult> Send<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
        var handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {
            throw new InvalidOperationException($"Handler not found for type {handlerType}");
        }

        return await (Task<TResult>)handlerType.GetMethod("Handle")!.Invoke(handler, new object[] { command, cancellationToken })!;
    }
}

