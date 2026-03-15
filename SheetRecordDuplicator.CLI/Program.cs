using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.Application.Extensions;
using SheetRecordDuplicator.CLI.Extensions;
using SheetRecordDuplicator.CLI.Workers;
using SheetRecordDuplicator.Infrastructure.Extensions;



var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddPresentation() 
    .AddApplication()
    .AddInfrastructure()
    .AddLogging(logBuilder => 
    {
        if(builder.Environment.IsProduction())
        {
            logBuilder.ClearProviders();
        }
    });

using (var host = builder.Build())
{
    using IServiceScope scope = host.Services.CreateScope();
    Worker worker = scope.ServiceProvider.GetRequiredService<Worker>();
    await worker.Run(args);
}