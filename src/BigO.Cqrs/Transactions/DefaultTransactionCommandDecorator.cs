namespace BigO.Cqrs.Transactions;

public class DefaultTransactionCommandDecorator<TCommand>
    (ICommandHandler<TCommand> decorated) : TransactionCommandDecoratorBase<TCommand>(decorated)
    where TCommand : class;