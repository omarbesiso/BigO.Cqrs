﻿using System.Diagnostics;
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
        _decorated = decorated;
        _logger = logger;
    }

    /// <summary>
    ///     Reads the specified query while applying decorator logic.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>TResult.</returns>
    public async Task<TResult> Read(TQuery query)
    {
        var queryName = query.GetType().Name;
        _logger.LogInformation("Start reading query '{queryName}'", queryName);

#if NET6_0
        var stopwatch = Stopwatch.StartNew();
        try
        {
            var result = await _decorated.Read(query);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while reading query '{queryName}'", queryName);
            throw;
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("Executed query '{queryName}' in {elapsedTime}.", queryName, stopwatch.Elapsed);
        }
#else
        var startTime = Stopwatch.GetTimestamp();
        try
        {
            var result = await _decorated.Read(query);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while reading query '{queryName}'", queryName);
            throw;
        }
        finally
        {
            var elapsedTime = Stopwatch.GetElapsedTime(startTime);
            _logger.LogInformation("Executed query '{queryName}' in {elapsedTime}.", queryName, elapsedTime);
        }
#endif
    }
}