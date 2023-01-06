using BigO.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Default implementation for the query processor using dependency injection for routing queries.
/// </summary>
internal class IocQueryProcessor : IQueryProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public IocQueryProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> ProcessQuery<TQuery, TResult>(TQuery query) where TQuery : class
    {
        Guard.NotNull(query, nameof(query));
        var queryHandler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return await queryHandler.Read(query);
    }
}