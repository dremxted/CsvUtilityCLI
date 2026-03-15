using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.Application.CommandHandlers;
using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.Application.Services;
using SheetRecordDuplicator.Common.Results;

namespace SheetRecordDuplicator.Application.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IGlobalCommandDispatcher, GlobalCommandDispatcher>();
        services.AddScoped<IGlobalCommandHandler<CombineStringsCommand, IEnumerable<string>>, CombineStringCommandHandler>();
        services.AddScoped<IGlobalCommandHandler<ReadFileAsyncCommand,Task<Result<string>>>,ReadFileAsyncCommandHandler>();
        services.AddScoped<IGlobalCommandHandler<GetExcelRowsAsyncCommand, Task<Result<IEnumerable<IDictionary<string, object>>>>>,GetExcelRowsAsyncCommandHandler>();
        services.AddScoped<IGlobalCommandHandler<WriteNewFileAsyncCommand, Task<Result>>,WriteNewFileAsyncCommandHandler>();
        return services;
    }
}
