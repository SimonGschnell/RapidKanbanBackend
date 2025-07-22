namespace RapidKanban.CQRS;

internal interface IQueryDispatcher
{
    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
}

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);
        if (handler is null)
        {
            throw new InvalidOperationException($"handler not found for type {handlerType}");
        }

        return await (Task<TResponse>)handlerType.GetMethod("Handle")!.Invoke(handler, new object[] { query, cancellationToken })!;
    }
}