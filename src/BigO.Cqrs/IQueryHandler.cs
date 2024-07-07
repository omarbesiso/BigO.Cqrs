using JetBrains.Annotations;

namespace BigO.Cqrs;

/// <summary>
///     Defines a contract for handling queries as specified in the CQRS pattern.
/// </summary>
/// <typeparam name="TQuery">The type of query to handle. Must be a reference type.</typeparam>
/// <typeparam name="TResult">The type of result to return.</typeparam>
/// <remarks>
///     The <see cref="IQueryHandler{TQuery,TResult}" /> interface defines a contract for handling a query of type
///     <typeparamref name="TQuery" /> and returns a result of type <typeparamref name="TResult" />.
///     Classes that implement this interface are responsible for defining the logic for handling the query and must
///     provide an implementation for the <see cref="Read" /> method.
///     <para>
///         Please note:
///     </para>
///     1. A query is a DTO representing a request for state data in the domain.
///     2. A single query usually is mapped to a single query handler.
///     3. In CQRS query handlers represent an implementation for Application Services (as defined in domain driven
///     design) that read state data in the domain.
/// </remarks>
[PublicAPI]
public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
{
    /// <summary>
    ///     Handles the query and returns back the relevant response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query. Must be a reference type.</typeparam>
    /// <typeparam name="TResult">The type of the query response.</typeparam>
    /// <param name="query">The query to be processed.</param>
    /// <returns>The relevant query response.</returns>
    Task<TResult> Read(TQuery query);
}