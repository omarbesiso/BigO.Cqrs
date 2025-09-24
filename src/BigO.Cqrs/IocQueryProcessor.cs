using BigO.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Default implementation for the query processor using dependency injection for routing queries.
/// </summary>
internal class IocQueryProcessor(IServiceProvider serviceProvider) : IQueryProcessor
{
    /// <inheritdoc />
    public async Task<TResult> ProcessQuery<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery
    {
        Guard.NotNull(query);
        var queryHandler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await queryHandler.Read(query, cancellationToken);
    }
}