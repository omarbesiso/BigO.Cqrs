﻿using System.Transactions;

namespace BigO.Cqrs.Transactions;

/// <summary>
///     An abstract base class for transaction command decorators. This class provides the implementation for handling
///     transactions around command execution.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public abstract class TransactionCommandDecoratorBase<TCommand>(ICommandHandler<TCommand> decorated) :
    ICommandDecorator<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    ///     The decorated command handler.
    /// </summary>
    protected readonly ICommandHandler<TCommand> Decorated = decorated;

    /// <summary>
    ///     Gets or sets the transaction options.
    /// </summary>
    public TransactionOptions TransactionOptions { get; protected set; } = new()
    {
        IsolationLevel = IsolationLevel.ReadCommitted,
        Timeout = TimeSpan.FromMinutes(1)
    };

    /// <summary>
    ///     Gets or sets the transaction scope option.
    /// </summary>
    public TransactionScopeOption TransactionScopeOption { get; protected set; } = TransactionScopeOption.Required;

    /// <summary>
    ///     Gets or sets the transaction scope async flow option.
    /// </summary>
    public TransactionScopeAsyncFlowOption TransactionScopeAsyncFlowOption { get; protected set; } =
        TransactionScopeAsyncFlowOption.Enabled;

    /// <summary>
    ///     Handles the specified command within a transaction scope.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    public async Task Handle(TCommand command)
    {
        using var scope =
            new TransactionScope(TransactionScopeOption, TransactionOptions, TransactionScopeAsyncFlowOption);
        await Decorated.Handle(command);
        scope.Complete();
    }
}