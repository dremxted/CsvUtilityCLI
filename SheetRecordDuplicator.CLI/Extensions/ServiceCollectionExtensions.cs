using Microsoft.Extensions.DependencyInjection;

namespace SheetRecordDuplicator.CLI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services
            .AddRootCommand()
            .AddCommandFactories()
            .AddCommandHandlers()
            .AddWorkers();
    }
}
