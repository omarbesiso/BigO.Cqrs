using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Class providing extensions to the <see cref="IServiceCollection" /> to allow for the decoration of different
///     types of handlers for different message types.
/// </summary>
[PublicAPI]
public static class DecorationServiceCollectionExtensions
{
    /// <summary>
    ///     Decorates a command handler for a given command type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the command handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler decorator added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a decorator
    ///     to a command handler service. The decorator wraps the command handler service, and can be used
    ///     to add additional behavior or functionality to the command handler.
    /// </remarks>
    public static IServiceCollection DecorateCommandHandler<TCommand, TDecorator>(
        this IServiceCollection serviceCollection)
        where TCommand : class
        where TDecorator : class, ICommandDecorator<TCommand>
    {
        return serviceCollection.Decorate<ICommandHandler<TCommand>, TDecorator>();
    }

    /// <summary>
    ///     Decorates an asynchronous command handler for a given command type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the command handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler decorator added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a decorator
    ///     to an asynchronous command handler service. The decorator wraps the command handler service, and
    ///     can be used to add additional behavior or functionality to the command handler.
    /// </remarks>
    public static IServiceCollection DecorateAsyncCommandHandler<TCommand, TDecorator>(
        this IServiceCollection serviceCollection)
        where TCommand : class
        where TDecorator : class, IAsyncCommandDecorator<TCommand>
    {
        return serviceCollection.Decorate<IAsyncCommandHandler<TCommand>, TDecorator>();
    }

    /// <summary>
    ///     Decorates a query handler for a given query type and result type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query handler.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the query handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler decorator added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a decorator
    ///     to a query handler service. The decorator wraps the query handler service, and can be used to
    ///     add additional behavior or functionality to the query handler.
    /// </remarks>
    public static IServiceCollection DecorateQueryHandler<TQuery, TResult, TDecorator>(
        this IServiceCollection serviceCollection)
        where TQuery : class
        where TDecorator : class, IQueryDecorator<TQuery, TResult>
    {
        return serviceCollection.Decorate<IQueryHandler<TQuery, TResult>, TDecorator>();
    }

    /// <summary>
    ///     Decorates an asynchronous query handler for a given query type and result type, with a specified decorator type.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query handler.</typeparam>
    /// <typeparam name="TDecorator">The type of the decorator being added to the query handler.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorator to.</param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler decorator added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a decorator
    ///     to an asynchronous query handler service. The decorator wraps the query handler service, and
    ///     can be used to add additional behavior or functionality to the query handler.
    /// </remarks>
    public static IServiceCollection DecorateAsyncQueryHandler<TQuery, TResult, TDecorator>(
        this IServiceCollection serviceCollection)
        where TQuery : class
        where TDecorator : class, IAsyncQueryDecorator<TQuery, TResult>
    {
        return serviceCollection.Decorate<IAsyncQueryHandler<TQuery, TResult>, TDecorator>();
    }
}