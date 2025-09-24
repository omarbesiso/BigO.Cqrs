using System.Diagnostics;
using BigO.Validation;
using Microsoft.Extensions.Logging;

namespace BigO.Cqrs.Logging;

/// <summary>
///     Provides a decorator for query handlers that logs entry, exit and errors resulting from processing a query as
///     well as provides execution elapsed time. This class cannot be inherited.
/// </summary>
/// <typeparam name="TQuery">The type of the query.</typeparam>
/// <typeparam name="TResult">The type of the result.</typeparam>
/// <seealso cref="IQueryDecorator{TQuery,TResult}" />
public sealed class LoggingQueryDecorator<TQuery, TResult> : IQueryDecorator<TQuery, TResult>
    where TQuery : IQuery
{
    private readonly IQueryHandler<TQuery, TResult> _decorated;
    private readonly ILogger<LoggingQueryDecorator<TQuery, TResult>> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoggingQueryDecorator{TQuery,TResult}" /> class.
    /// </summary>
    /// <param name="decorated">The decorated query handler.</param>
    /// <param name="logger">The configured logger.</param>
    public LoggingQueryDecorator(IQueryHandler<TQuery, TResult> decorated,
        ILogger<LoggingQueryDecorator<TQuery, TResult>> logger)
    {
        // Consider adding Guard.Against.Null(decorated); and Guard.Against.Null(logger);
        // if that's a pattern used elsewhere in your library.
        _decorated = decorated;
        _logger = logger;
    }

    /// <summary>
    ///     Reads the specified query while applying decorator logic.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the query.</returns>
    public async Task<TResult> Read(TQuery query, CancellationToken cancellationToken = default)
    {
        Guard.NotNull(query);

        var queryName = query.GetType().Name;
        CqrsLog.StartReadingQuery(_logger, queryName);

        var startTime = Stopwatch.GetTimestamp();
        try
        {
            var result = await _decorated.Read(query, cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            CqrsLog.ErrorReadingQuery(_logger, queryName, ex);
            throw;
        }
        finally
        {
            var elapsedTime = Stopwatch.GetElapsedTime(startTime);
            CqrsLog.ExecutedQuery(_logger, queryName, elapsedTime);
        }
    }
}