using BigO.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Default implementation for the query processor using dependency injection for routing queries.
/// </summary>
internal class IocQueryProcessor(IServiceProvider serviceProvider) : IQueryProcessor
{
    /// <summary>
    ///     Processes the query by routing it to a query handler and returning a result.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query. Must be a reference type.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query to be routed.</param>
    /// <returns>The relevant query response.</returns>
    public async Task<TResult> ProcessQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        Guard.NotNull(query, nameof(query));
        var queryHandler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await queryHandler.Read(query);
    }
}