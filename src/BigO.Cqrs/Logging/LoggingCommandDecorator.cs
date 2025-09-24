using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BigO.Cqrs.Logging;

/// <summary>
///     Provides a decorator for command handlers that logs entry, exit and errors resulting from executing a command as
///     well as provides execution elapsed time. This class cannot be inherited.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <seealso cref="ICommandDecorator{TCommand}" />
public sealed class LoggingCommandDecorator<TCommand> : ICommandDecorator<TCommand>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _decorated;

    // MODIFIED: Logger field type made more specific
    private readonly ILogger<LoggingCommandDecorator<TCommand>> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoggingCommandDecorator{TCommand}" /> class.
    /// </summary>
    /// <param name="decorated">The decorated command handler.</param>
    /// <param name="logger">The configured logger.</param>
    public LoggingCommandDecorator(ICommandHandler<TCommand> decorated,
        ILogger<LoggingCommandDecorator<TCommand>> logger)
    {
        // Consider adding Guard.Against.Null(decorated); and Guard.Against.Null(logger);
        _decorated = decorated;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the specified command while applying decorator logic.
    /// </summary>
    /// <param name="command">The command to be executed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task Handle(TCommand command, CancellationToken cancellationToken = default)
    {
        var commandName = command.GetType().Name;
        CqrsLog.StartExecutingCommand(_logger, commandName); // MODIFIED: Using source-generated logger

        var startTime = Stopwatch.GetTimestamp();
        try
        {
            // MODIFIED: Added ConfigureAwait(false)
            await _decorated.Handle(command, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            CqrsLog.ErrorExecutingCommand(_logger, commandName, ex); // MODIFIED: Using source-generated logger
            throw;
        }
        finally
        {
            var elapsedTime = Stopwatch.GetElapsedTime(startTime);
            CqrsLog.ExecutedCommand(_logger, commandName, elapsedTime); // MODIFIED: Using source-generated logger
        }
    }
}