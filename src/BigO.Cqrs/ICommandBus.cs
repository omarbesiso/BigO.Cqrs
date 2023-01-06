using JetBrains.Annotations;

namespace BigO.Cqrs;

/// <summary>
///     The contract defines the router of commands to command handlers.
/// </summary>
[PublicAPI]
public interface ICommandBus
{
    /// <summary>
    ///     Routes the specified command to the relevant command handler.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to be routed.</param>
    Task Send<TCommand>(TCommand command) where TCommand : class;
}