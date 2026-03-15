using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.CLI.Commands.CLI;

namespace SheetRecordDuplicator.CLI.Workers;

internal class Worker
{
    private ApplicationRootCommand _rootCommand;
    public Worker(
        ApplicationRootCommand rootCommand, 
        ILogger<ApplicationRootCommand> logger)
    {
        _rootCommand = rootCommand;
    }

    internal async Task Run(string[] args)
    { 
        await _rootCommand
            .Parse(args)
            .InvokeAsync();
    }
}
