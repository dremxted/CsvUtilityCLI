using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.CLI.Commands.CQRS;
using SheetRecordDuplicator.Common.Results;
using System.Text;

namespace SheetRecordDuplicator.CLI.CommandHandlers;

internal class RootActionCommandHandler : IGlobalCommandHandler<RootActionCommand, Task<int>>
{
    private ILogger<RootActionCommandHandler> _logger;
    private IGlobalCommandDispatcher _commandDispatcher;
    public RootActionCommandHandler(
        ILogger<RootActionCommandHandler> logger,
        IGlobalCommandDispatcher commandDispatcher)
    {
        _logger = logger;
        _commandDispatcher = commandDispatcher;
    }
    public async Task<int> Handle(
        RootActionCommand command, 
        CancellationToken cancellationToken = default)
    {
        FileInfo templateFileInfo = command.ParseResult.GetValue<FileInfo>(
            Constants.TemplateFileArgument)!;
        FileInfo inputsFileInfo = command.ParseResult.GetValue<FileInfo>(
            Constants.InputsFileArgument)!;
        FileInfo outputFileInfo = command.ParseResult.GetValue<FileInfo>(
            Constants.OutputOption)!;

        var templateResult = _commandDispatcher
            .Dispatch<ReadFileAsyncCommand,Task<Result<string>>>(
            new ReadFileAsyncCommand(templateFileInfo.FullName), cancellationToken);

        var inputsResult = _commandDispatcher
            .Dispatch<GetExcelRowsAsyncCommand,Task<Result<IEnumerable<IDictionary<string, object>>>>>(
            new GetExcelRowsAsyncCommand(inputsFileInfo.FullName), cancellationToken);

        Result<StringBuilder> combinedResult = CombineTemplateInputs(
                (await templateResult).Value,
                (await inputsResult).Value,
                cancellationToken);

        var writeFileResult = await _commandDispatcher
            .Dispatch<WriteNewFileAsyncCommand,Task<Result>>(
            new WriteNewFileAsyncCommand(outputFileInfo, combinedResult.Value), cancellationToken);

        return writeFileResult.Match(
            () =>
            {
                Console.WriteLine(outputFileInfo);
                return 0;
            },
            error =>
            {
                Console.WriteLine(error.Message);
                return 1;
            });
    }

    private Result<StringBuilder> CombineTemplateInputs(
        string template, 
        IEnumerable<IDictionary<string,object>> inputs,
        CancellationToken cancellationToken)
    {
        StringBuilder csvCombinedSb = new();
        try
        {
            foreach (var item in _commandDispatcher
                .Dispatch<CombineStringsCommand, IEnumerable<string>>(
                    new CombineStringsCommand(template, inputs),
                    cancellationToken))
            {
                csvCombinedSb.Append(item);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType().ToString()} {ex.Message}");
            return Result.Failure<StringBuilder>(Error.Exception($"{ex.Message}"));
        }
        return Result.Success(csvCombinedSb);
    }
}