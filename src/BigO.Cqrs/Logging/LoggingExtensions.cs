using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs.Logging;

/// <summary>
///     Provides extension methods for decorating CQRS command and query handlers with logging capabilities.
/// </summary>
public static class LoggingExtensions
{
    /// <summary>
    ///     Decorates the command handler with logging.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="serviceCollection">The service collection to contain the registration.</param>
    /// <returns>A reference to this service collection instance after the operation has completed.</returns>
    public static IServiceCollection DecorateCommandHandlerWithLogging<TCommand>(
        this IServiceCollection serviceCollection)
        where TCommand : ICommand =>
        serviceCollection.DecorateCommandHandler<TCommand, LoggingCommandDecorator<TCommand>>();

    /// <summary>
    ///     Decorates the query handler with logging.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="serviceCollection">The service collection to contain the registration.</param>
    /// <returns>A reference to this service collection instance after the operation has completed.</returns>
    public static IServiceCollection DecorateQueryHandlerWithLogging<TQuery, TResult>(
        this IServiceCollection serviceCollection)
        where TQuery : IQuery =>
        serviceCollection.DecorateQueryHandler<TQuery, TResult, LoggingQueryDecorator<TQuery, TResult>>();
}