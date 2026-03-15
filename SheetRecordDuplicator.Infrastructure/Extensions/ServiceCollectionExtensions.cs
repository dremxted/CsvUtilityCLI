using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.Application.Contracts.Services;
using SheetRecordDuplicator.Infrastructure.Repositories;
using SheetRecordDuplicator.Infrastructure.Services;

namespace SheetRecordDuplicator.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITextFileReader, TextFileReader>();
        services.AddScoped<ITextFileWriter, TextFileWriter>();
        services.AddScoped<ICsvReader, CsvReader>();
        return services;
    }
}
