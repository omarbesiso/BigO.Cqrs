using BigO.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace BigO.Cqrs;

/// <summary>
///     Default implementation for the command bus using dependency injection for routing commands.
/// </summary>
internal class IocCommandBus(IServiceProvider serviceProvider) : ICommandBus
{
    public async Task Send<TCommand>(TCommand command) where TCommand : class
    {
        Guard.NotNull(command);
        var commandHandler = serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await commandHandler.Handle(command);
    }
}