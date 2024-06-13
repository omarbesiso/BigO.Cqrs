using System.Diagnostics;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedMember.Global

namespace BigO.Cqrs.Logging;

/// <summary>
///     Provides a decorator for command handlers that logs entry, exit and errors resulting from executing a command as
///     well as provides execution elapsed time. This class cannot be inherited.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <seealso cref="ICommandDecorator{TCommand}" />
public sealed class LoggingCommandDecorator<TCommand> : ICommandDecorator<TCommand>
    where TCommand : class
{
    private readonly ICommandHandler<TCommand> _decorated;
    private readonly ILogger _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LoggingCommandDecorator{TCommand}" /> class.
    /// </summary>
    /// <param name="decorated">The decorated command handler.</param>
    /// <param name="logger">The configured logger.</param>
    public LoggingCommandDecorator(ICommandHandler<TCommand> decorated,
        ILogger<LoggingCommandDecorator<TCommand>> logger)
    {
        _decorated = decorated;
        _logger = logger;
    }

    /// <summary>
    ///     Handles the specified command while applying decorator logic.
    /// </summary>
    /// <param name="command">The command to be executed.</param>
    public async Task Handle(TCommand command)
    {
        var commandName = command.GetType().Name;

        _logger.LogInformation("Start executing command '{commandName}'", commandName);

        var startTime = Stopwatch.GetTimestamp();

        try
        {
            await _decorated.Handle(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception throw while executing command '{commandName}'", commandName);
            throw;
        }

        var elapsedTime = Stopwatch.GetElapsedTime(startTime);
        _logger.LogInformation("Executed command '{commandName}' in {elapsedTime}.", commandName, elapsedTime);
    }
}