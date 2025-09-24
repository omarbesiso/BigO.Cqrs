﻿namespace BigO.Cqrs;

/// <summary>
///     Defines a decorator for command handlers. Each decorator provides the ability to add additional functionality to
///     command handlers while not violating the single responsibility principle or the open/closed principle for the
///     S.O.L.I.D design patterns.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandDecorator<in TCommand> : ICommandHandler<TCommand> where TCommand : ICommand;