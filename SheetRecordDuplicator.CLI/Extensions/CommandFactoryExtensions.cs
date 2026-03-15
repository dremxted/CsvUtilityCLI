using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.CLI.Contracts;
using SheetRecordDuplicator.CLI.Factories;
using SheetRecordDuplicator.CLI.Commands.CLI;

namespace SheetRecordDuplicator.CLI.Extensions;

internal static class CommandFactoryExtensions
{
    internal static IServiceCollection AddCommandFactories(this IServiceCollection services)
    {
        services.AddScoped<ICommandFactory<ApplicationRootCommand>, SheetRecordDuplicatorFactory>();
        return services;
    }
}
