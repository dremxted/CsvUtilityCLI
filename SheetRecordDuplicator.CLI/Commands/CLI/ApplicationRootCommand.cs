using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.CLI.Commands.CQRS;
using SheetRecordDuplicator.CLI.Contracts;
using System.CommandLine;

namespace SheetRecordDuplicator.CLI.Commands.CLI;

internal class ApplicationRootCommand : RootCommand
{
    private ICommandFactory<ApplicationRootCommand> _commandFactory;
    private IGlobalCommandDispatcher _commandDispatcher;

    public ApplicationRootCommand(
        ICommandFactory<ApplicationRootCommand> commandFactory,
        IGlobalCommandDispatcher commandDisptcher)
    {
        _commandFactory = commandFactory;
        _commandDispatcher = commandDisptcher;
        Init();
    }

    private void Init()
    {
        Description = "A tool to duplicate data within a table sheet based on the inputs sheet.";

        foreach (var argument in _commandFactory.CreateArguments())
        {
            Arguments.Add(argument);
        }

        foreach (var option in _commandFactory.CreateOptions())
        {
            Options.Add(option);
        }
        SetAction(CommandHandlerAdapter);
    }

    private async Task<int> CommandHandlerAdapter(
        ParseResult parseResult, 
        CancellationToken cancellationToken)
    {
        return await _commandDispatcher.Dispatch<RootActionCommand,Task<int>>(
            new RootActionCommand(parseResult),
            cancellationToken);
    }
}
