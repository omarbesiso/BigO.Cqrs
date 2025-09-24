using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Class providing extensions to the <see cref="IServiceCollection" /> to allow for the decoration of different
///     types of handlers for different message types.
/// </summary>
public static class DecorationServiceCollectionExtensions
{
    /// <summary>
    ///     Decorates a command handler for a given command type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the command handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler decorator added.</returns>
    public static IServiceCollection DecorateCommandHandler<TCommand, TDecorator>(
        this IServiceCollection serviceCollection)
        where TCommand : ICommand
        where TDecorator : class, ICommandDecorator<TCommand> =>
        serviceCollection.Decorate<ICommandHandler<TCommand>, TDecorator>();

    /// <summary>
    ///     Decorates a query handler for a given query type and result type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query handler.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the query handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler decorator added.</returns>
    public static IServiceCollection DecorateQueryHandler<TQuery, TResult, TDecorator>(
        this IServiceCollection serviceCollection)
        where TQuery : IQuery
        where TDecorator : class, IQueryDecorator<TQuery, TResult> =>
        serviceCollection.Decorate<IQueryHandler<TQuery, TResult>, TDecorator>();
}