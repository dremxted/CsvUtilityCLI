using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.CLI.Workers;

namespace SheetRecordDuplicator.CLI.Extensions;

internal static class WorkerExtensions
{
    internal static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services.AddScoped<Worker>();
        return services;
    }
}
