namespace BigO.Cqrs.Transactions;

/// <summary>
///     A default implementation of the <see cref="TransactionCommandDecoratorBase{TCommand}" /> which provides transaction
///     handling for command handlers.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public class DefaultTransactionCommandDecorator<TCommand>(ICommandHandler<TCommand> decorated)
    : TransactionCommandDecoratorBase<TCommand>(decorated)
    where TCommand : class;