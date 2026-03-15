using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.CLI.CommandHandlers;
using SheetRecordDuplicator.CLI.Commands.CQRS;

namespace SheetRecordDuplicator.CLI.Extensions;

internal static class CommandHandlerExtensions
{
    internal static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<IGlobalCommandHandler<RootActionCommand, Task<int>>, RootActionCommandHandler>();
        return services;
    }
}
