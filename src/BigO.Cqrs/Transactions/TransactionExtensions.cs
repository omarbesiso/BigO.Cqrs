using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs.Transactions;

/// <summary>
///     Provides extension methods for decorating command handlers with transaction support.
/// </summary>
[PublicAPI]
public static class TransactionExtensions
{
    /// <summary>
    ///     Decorates the command handler for the specified command type with a transaction decorator.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="serviceCollection">The service collection to add the decorated command handler to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection DecorateCommandHandlerWithTransactions<TCommand>(
        this IServiceCollection serviceCollection)
        where TCommand : class
    {
        return serviceCollection.DecorateCommandHandler<TCommand, DefaultTransactionCommandDecorator<TCommand>>();
    }
}