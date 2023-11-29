using System.Transactions;

namespace BigO.Cqrs.Transactions;

public abstract class TransactionCommandDecoratorBase<TCommand>(ICommandHandler<TCommand> decorated) :
    ICommandDecorator<TCommand>
    where TCommand : class
{
    protected readonly ICommandHandler<TCommand> Decorated = decorated;

    public TransactionOptions TransactionOptions { get; protected set; } = new()
    {
        IsolationLevel = IsolationLevel.ReadCommitted,
        Timeout = TimeSpan.FromMinutes(1)
    };

    public TransactionScopeOption TransactionScopeOption { get; protected set; } = TransactionScopeOption.Required;

    public TransactionScopeAsyncFlowOption TransactionScopeAsyncFlowOption { get; protected set; } =
        TransactionScopeAsyncFlowOption.Enabled;

    public async Task Handle(TCommand command)
    {
        using var scope =
            new TransactionScope(TransactionScopeOption, TransactionOptions, TransactionScopeAsyncFlowOption);
        await Decorated.Handle(command);
        scope.Complete();
    }
}