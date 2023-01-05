using JetBrains.Annotations;

namespace BigO.Cqrs;

/// <summary>
///     Defines a contract for asynchronously handling command as specified in the CQRS pattern.
/// </summary>
/// <typeparam name="TCommand">The type of command to handle. Must be a reference type.</typeparam>
/// <remarks>
///     The <see cref="ICommandHandler{TCommand}" /> interface defines a contract for asynchronously handling a command of
///     type
///     <typeparamref name="TCommand" />.
///     Classes that implement this interface are responsible for defining the logic for asynchronously handling the
///     command and must provide an implementation for the <see cref="HandleAsync" /> method.
///     <para>
///         Please note:
///     </para>
///     1. A command is a DTO representing a request for a change in the domain.
///     2. A single command usually is mapped to a single command handler.
///     3. In CQRS command handlers represent an implementation for Application Services (as defined in domain driven
///     design) that cause a change in the domain.
/// </remarks>
[PublicAPI]
public interface IAsyncCommandHandler<in TCommand> where TCommand : class
{
    /// <summary>
    ///     Handles the specified command asynchronously.
    /// </summary>
    /// <param name="command">The command.</param>
    Task HandleAsync(TCommand command);
}