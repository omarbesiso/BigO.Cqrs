using BigO.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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

    /// <summary>
    ///     Registers all query decorators in the specified module.
    /// </summary>
    /// <typeparam name="TModule">The type of the module to register the query decorators from.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service decorators to.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterModuleQueryDecorators<TModule>(
        this IServiceCollection serviceCollection)
        where TModule : IModule
    {
        // 1. Identify the interface we care about
        var decoratorInterface = typeof(IQueryDecorator<,>);

        // 2. Get all relevant assemblies for the module
        var moduleAssembly = typeof(TModule).Assembly;

        // 3. Find all concrete classes that implement IQueryDecorator<TQuery, TResult>
        var decoratorTypes = moduleAssembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => t.GetInterfaces().Any(IsMatchingDecoratorInterface));

        // 4. For each decorator, figure out the type parameters <TQuery, TResult>
        foreach (var decoratorType in decoratorTypes)
        {
            // Find exactly which IQueryDecorator<,> interface it implements
            var iQueryDecorator = decoratorType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType
                                     && i.GetGenericTypeDefinition() == decoratorInterface);

            if (iQueryDecorator is null)
            {
                continue; // shouldn't happen with the .Any() filter, but just in case
            }

            var genericArgs = iQueryDecorator.GetGenericArguments();
            if (genericArgs.Length != 2)
            {
                continue; // safety check
            }

            var queryType = genericArgs[0];
            var resultType = genericArgs[1];

            // 5. Call .DecorateQueryHandler<TQuery, TResult, TDecorator>() via reflection on *this* class
            typeof(DecorationServiceCollectionExtensions)
                .GetMethod(nameof(DecorateQueryHandler), BindingFlags.Static | BindingFlags.Public)!
                .MakeGenericMethod(queryType, resultType, decoratorType)
                .Invoke(null, [serviceCollection]);
        }

        return serviceCollection;
    }

    private static bool IsMatchingDecoratorInterface(Type i) =>
        i.IsGenericType
        && i.GetGenericTypeDefinition() == typeof(IQueryDecorator<,>);
}