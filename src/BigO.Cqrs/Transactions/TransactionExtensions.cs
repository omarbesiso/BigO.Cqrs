using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs.Transactions;

[PublicAPI]
public static class TransactionExtensions
{
    public static IServiceCollection DecorateCommandHandlerWithTransactions<TCommand>(
        this IServiceCollection serviceCollection)
        where TCommand : class
    {
        return serviceCollection.DecorateCommandHandler<TCommand, DefaultTransactionCommandDecorator<TCommand>>();
    }
}