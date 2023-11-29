using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs.Logging;

[PublicAPI]
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
        where TCommand : class
    {
        return serviceCollection.DecorateCommandHandler<TCommand, LoggingCommandDecorator<TCommand>>();
    }

    /// <summary>
    ///     Decorates the query handler with logging.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="serviceCollection">The service collection to contain the registration.</param>
    /// <returns>A reference to this service collection instance after the operation has completed.</returns>
    public static IServiceCollection DecorateQueryHandlerWithLogging<TQuery, TResult>(
        this IServiceCollection serviceCollection)
        where TQuery : class
    {
        return serviceCollection.DecorateQueryHandler<TQuery, TResult, LoggingQueryDecorator<TQuery, TResult>>();
    }
}