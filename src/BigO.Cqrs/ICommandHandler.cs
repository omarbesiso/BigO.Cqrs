namespace BigO.Cqrs;

/// <summary>
///     Defines a contract for handling command as specified in the CQRS pattern.
/// </summary>
/// <typeparam name="TCommand">The type of command to handle, which must implement <see cref="ICommand" />.</typeparam>
/// <remarks>
///     The <see cref="ICommandHandler{TCommand}" /> interface defines a contract for handling a command of type
///     <typeparamref name="TCommand" />.
///     Classes that implement this interface are responsible for defining the logic for handling the command and must
///     provide an implementation for the <see cref="Handle" /> method.
///     <para>
///         Please note:
///     </para>
///     1. A command is a DTO representing a request for a change in the domain.
///     2. A single command usually is mapped to a single command handler.
///     3. In CQRS command handlers represent an implementation for Application Services (as defined in domain driven
///     design) that cause a change in the domain.
/// </remarks>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    ///     Handles the specified command.
    /// </summary>
    /// <param name="command">The command to be handled.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}