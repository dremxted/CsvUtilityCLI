using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.CLI.Commands.CLI;

namespace SheetRecordDuplicator.CLI.Extensions;

internal static class CommandExtensions
{
    internal static IServiceCollection AddRootCommand(this IServiceCollection services)
    {
        services.AddScoped<ApplicationRootCommand>();
        return services;
    }
}