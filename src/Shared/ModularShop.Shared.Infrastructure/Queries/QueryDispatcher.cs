using Microsoft.Extensions.DependencyInjection;
using ModularShop.Shared.Abstractions.Queries;

namespace ModularShop.Shared.Infrastructure.Queries;

internal sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        return await ((Task<TResult>) handlerType
            .GetMethod(nameof(IQueryHandler<,>.HandleAsync))
            ?.Invoke(handler, [query, cancellationToken])!);
    }
}