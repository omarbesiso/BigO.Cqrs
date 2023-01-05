namespace BigO.Cqrs;

/// <summary>
///     Defines a decorator for async handlers. Each decorator provides the ability to add additional functionality to
///     command handlers while not violating the single responsibility principle or the open/closed principle for the
///     S.O.L.I.D design patterns.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface IAsyncCommandDecorator<in TCommand> : IAsyncCommandHandler<TCommand>
    where TCommand : class
{
}