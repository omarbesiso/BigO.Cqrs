using BigO.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Default implementation for the query processor using dependency injection for routing queries.
/// </summary>
internal class IocQueryProcessor(IServiceProvider serviceProvider) : IQueryProcessor
{
    public async Task<TResult> ProcessQuery<TQuery, TResult>(TQuery query) where TQuery : class
    {
        Guard.NotNull(query, nameof(query));
        var queryHandler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await queryHandler.Read(query);
    }
}