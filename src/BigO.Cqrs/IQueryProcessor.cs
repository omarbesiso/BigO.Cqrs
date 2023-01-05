using JetBrains.Annotations;

namespace BigO.Cqrs;

/// <summary>
///     The contract defines the router of queries to query handlers.
/// </summary>
[PublicAPI]
public interface IQueryProcessor
{
    /// <summary>
    ///     Processes the query by routing it to a query handler and returning a result..
    /// </summary>
    /// <typeparam name="TQuery">The type of the query. Must be a reference type.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query to be routed.</param>
    /// <returns>The relevant query response.</returns>
    TResult ProcessQuery<TQuery, TResult>(TQuery query) where TQuery : class;

    /// <summary>
    ///     Processes the query by asynchronously routing it to a query handler and returning a result.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query. Must be a reference type.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="query">The query to be asynchronously routed.</param>
    /// <returns>The relevant query response.</returns>
    Task<TResult> ProcessAsyncQuery<TQuery, TResult>(TQuery query) where TQuery : class;
}