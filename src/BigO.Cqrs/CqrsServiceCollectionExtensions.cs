﻿using BigO.DependencyInjection;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Class providing extensions to the <see cref="IServiceCollection" /> to allow for the registration of different
///     types of handlers for different message types.
/// </summary>
[PublicAPI]
public static class CqrsServiceCollectionExtensions
{
    /// <summary>
    ///     Registers a command handler for a given command type, with the specified service lifetime.
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled.</typeparam>
    /// <typeparam name="TCommandHandler">The type of the command handler being registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the command handler service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a command handler
    ///     service to the collection. The command handler service is registered with the specified lifetime,
    ///     which determines the lifetime of the service instance. The service instance will be created when
    ///     it is first requested, and will be disposed of according to the specified lifetime.
    /// </remarks>
    public static IServiceCollection RegisterCommandHandler<TCommand, TCommandHandler>(
        this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TCommand : class
        where TCommandHandler : class, ICommandHandler<TCommand>
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Singleton =>
                serviceCollection.AddSingleton<ICommandHandler<TCommand>, TCommandHandler>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<ICommandHandler<TCommand>, TCommandHandler>(),
            ServiceLifetime.Transient =>
                serviceCollection.AddTransient<ICommandHandler<TCommand>, TCommandHandler>(),
            _ => serviceCollection.AddTransient<ICommandHandler<TCommand>, TCommandHandler>()
        };
    }

    /// <summary>
    ///     Registers an asynchronous command handler for a given command type, with the specified service lifetime.
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled.</typeparam>
    /// <typeparam name="TCommandHandler">The type of the command handler being registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the command handler service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds an asynchronous
    ///     command handler service to the collection. The command handler service is registered with the
    ///     specified lifetime, which determines the lifetime of the service instance. The service instance
    ///     will be created when it is first requested, and will be disposed of according to the specified
    ///     lifetime.
    /// </remarks>
    public static IServiceCollection RegisterAsyncCommandHandler<TCommand, TCommandHandler>(
        this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TCommand : class
        where TCommandHandler : class, IAsyncCommandHandler<TCommand>
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Singleton =>
                serviceCollection.AddSingleton<IAsyncCommandHandler<TCommand>, TCommandHandler>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<IAsyncCommandHandler<TCommand>, TCommandHandler>(),
            ServiceLifetime.Transient =>
                serviceCollection.AddTransient<IAsyncCommandHandler<TCommand>, TCommandHandler>(),
            _ => serviceCollection.AddTransient<IAsyncCommandHandler<TCommand>, TCommandHandler>()
        };
    }

    /// <summary>
    ///     Registers the default command bus service, with the specified service lifetime.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the command bus service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Singleton" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the command bus service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds the default
    ///     command bus service to the collection. The command bus service is registered with the specified
    ///     lifetime, which determines the lifetime of the service instance. The service instance will be
    ///     created when it is first requested, and will be disposed of according to the specified lifetime.
    /// </remarks>
    public static IServiceCollection RegisterDefaultCommandBus(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Transient => serviceCollection.AddTransient<ICommandBus, IocCommandBus>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<ICommandBus, IocCommandBus>(),
            ServiceLifetime.Singleton => serviceCollection.AddSingleton<ICommandBus, IocCommandBus>(),
            _ => serviceCollection.AddSingleton<ICommandBus, IocCommandBus>()
        };
    }

    /// <summary>
    ///     Registers a query handler for a given query type, with the specified service lifetime.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query handler.</typeparam>
    /// <typeparam name="TQueryHandler">The type of the query handler being registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the query handler service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds a query handler
    ///     service to the collection. The query handler service is registered with the specified lifetime,
    ///     which determines the lifetime of the service instance. The service instance will be created when
    ///     it is first requested, and will be disposed of according to the specified lifetime.
    /// </remarks>
    public static IServiceCollection RegisterQueryHandler<TQuery, TResult, TQueryHandler>(
        this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TQuery : class
        where TQueryHandler : class, IQueryHandler<TQuery, TResult>
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Singleton =>
                serviceCollection.AddSingleton<IQueryHandler<TQuery, TResult>, TQueryHandler>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<IQueryHandler<TQuery, TResult>, TQueryHandler>(),
            ServiceLifetime.Transient =>
                serviceCollection.AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>(),
            _ => serviceCollection.AddTransient<IQueryHandler<TQuery, TResult>, TQueryHandler>()
        };
    }

    /// <summary>
    ///     Registers an asynchronous query handler for a given query type, with the specified service lifetime.
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the query handler.</typeparam>
    /// <typeparam name="TQueryHandler">The type of the query handler being registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the query handler service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds an asynchronous
    ///     query handler service to the collection. The query handler service is registered with the specified
    ///     lifetime, which determines the lifetime of the service instance. The service instance will be
    ///     created when it is first requested, and will be disposed of according to the specified lifetime.
    /// </remarks>
    public static IServiceCollection RegisterAsyncQueryHandler<TQuery, TResult, TQueryHandler>(
        this IServiceCollection serviceCollection, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TQuery : class
        where TQueryHandler : class, IAsyncQueryHandler<TQuery, TResult>
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Singleton => serviceCollection
                .AddSingleton<IAsyncQueryHandler<TQuery, TResult>, TQueryHandler>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<IAsyncQueryHandler<TQuery, TResult>, TQueryHandler>(),
            ServiceLifetime.Transient => serviceCollection
                .AddTransient<IAsyncQueryHandler<TQuery, TResult>, TQueryHandler>(),
            _ => serviceCollection.AddTransient<IAsyncQueryHandler<TQuery, TResult>, TQueryHandler>()
        };
    }

    /// <summary>
    ///     Registers the default query processor service, with the specified service lifetime.
    /// </summary>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to add the service to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the query processor service.
    ///     If not specified, the default value is <see cref="ServiceLifetime.Singleton" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the query processor service added.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that adds the default
    ///     query processor service to the collection. The query processor service is registered with the
    ///     specified lifetime, which determines the lifetime of the service instance. The service instance
    ///     will be created when it is first requested, and will be disposed of according to the specified
    ///     lifetime.
    /// </remarks>
    public static IServiceCollection RegisterDefaultQueryProcessor(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        return serviceLifetime switch
        {
            ServiceLifetime.Transient => serviceCollection.AddTransient<IQueryProcessor, IocQueryProcessor>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<IQueryProcessor, IocQueryProcessor>(),
            ServiceLifetime.Singleton => serviceCollection.AddSingleton<IQueryProcessor, IocQueryProcessor>(),
            _ => serviceCollection.AddSingleton<IQueryProcessor, IocQueryProcessor>()
        };
    }

    /// <summary>
    ///     Registers all command handlers within a given module, with a specified service lifetime.
    /// </summary>
    /// <typeparam name="TModule">The type of the module containing the command handlers to be registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to register the command handlers to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the command handler services to be registered.
    ///     The default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler services registered.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that registers all
    ///     command handler services within a given module. The command handlers must implement
    ///     <see cref="ICommandHandler{TCommand}" />, where TCommand is the command type being handled.
    ///     The service lifetime of the command handler services is specified by the <paramref name="serviceLifetime" />
    ///     parameter.
    /// </remarks>
    public static IServiceCollection RegisterModuleCommandHandlers<TModule>(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TModule : IModule
    {
        var type = typeof(ICommandHandler<>);
        RegisterInternal<TModule>(serviceCollection, serviceLifetime, type);
        return serviceCollection;
    }

    /// <summary>
    ///     Registers all asynchronous command handlers within a given module, with a specified service lifetime.
    /// </summary>
    /// <typeparam name="TModule">The type of the module containing the asynchronous command handlers to be registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to register the command handlers to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the command handler services to be registered.
    ///     The default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the command handler services registered.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that registers all
    ///     asynchronous command handler services within a given module. The command handlers must implement
    ///     <see cref="IAsyncCommandHandler{TCommand}" />, where TCommand is the command type being handled.
    ///     The service lifetime of the command handler services is specified by the <paramref name="serviceLifetime" />
    ///     parameter.
    /// </remarks>
    public static IServiceCollection RegisterModuleAsyncCommandHandlers<TModule>(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TModule : IModule
    {
        var type = typeof(IAsyncCommandHandler<>);
        RegisterInternal<TModule>(serviceCollection, serviceLifetime, type);
        return serviceCollection;
    }

    /// <summary>
    ///     Registers all query handlers within a given module, with a specified service lifetime.
    /// </summary>
    /// <typeparam name="TModule">The type of the module containing the query handlers to be registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to register the query handlers to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the query handler services to be registered.
    ///     The default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the query handler services registered.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that registers all query handler services
    ///     within a given module. The query handlers must implement <see cref="IQueryHandler{TQuery, TResult}" />, where
    ///     TQuery
    ///     is the query type being handled and TResult is the result type of the query. The service lifetime of the query
    ///     handler services is specified by the <paramref name="serviceLifetime" /> parameter.
    /// </remarks>
    public static IServiceCollection RegisterModuleQueryHandlers<TModule>(this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TModule : IModule
    {
        var type = typeof(IQueryHandler<,>);
        RegisterInternal<TModule>(serviceCollection, serviceLifetime, type);
        return serviceCollection;
    }

    /// <summary>
    ///     Registers all asynchronous query handlers within a given module, with a specified service lifetime.
    /// </summary>
    /// <typeparam name="TModule">The type of the module containing the async query handlers to be registered.</typeparam>
    /// <param name="serviceCollection">The <see cref="IServiceCollection" /> to register the async query handlers to.</param>
    /// <param name="serviceLifetime">
    ///     The <see cref="ServiceLifetime" /> of the async query handler services to be registered.
    ///     The default value is <see cref="ServiceLifetime.Transient" />.
    /// </param>
    /// <returns>The <paramref name="serviceCollection" /> with the async query handler services registered.</returns>
    /// <remarks>
    ///     This method is an extension method for <see cref="IServiceCollection" /> that registers all query handler services
    ///     within a given module. The query handlers must implement <see cref="IAsyncQueryHandler{TQuery, TResult}" />, where
    ///     TQuery
    ///     is the query type being handled and TResult is the result type of the query. The service lifetime of the query
    ///     handler services is specified by the <paramref name="serviceLifetime" /> parameter.
    /// </remarks>
    public static IServiceCollection RegisterModuleAsyncQueryHandlers<TModule>(
        this IServiceCollection serviceCollection,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TModule : IModule
    {
        var type = typeof(IAsyncQueryHandler<,>);
        RegisterInternal<TModule>(serviceCollection, serviceLifetime, type);
        return serviceCollection;
    }

    private static void RegisterInternal<TModule>(IServiceCollection serviceCollection, ServiceLifetime serviceLifetime,
        Type type)
        where TModule : IModule
    {
        switch (serviceLifetime)
        {
            case ServiceLifetime.Scoped:
                serviceCollection.Scan(scan =>
                    scan.FromAssemblyOf<TModule>()
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
                break;
            case ServiceLifetime.Transient:
                serviceCollection.Scan(scan =>
                    scan.FromAssemblyOf<TModule>()
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime());
                break;
            case ServiceLifetime.Singleton:
                serviceCollection.Scan(scan =>
                    scan.FromAssemblyOf<TModule>()
                        .AddClasses(classes => classes.AssignableTo(type))
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime());
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null);
        }
    }
}