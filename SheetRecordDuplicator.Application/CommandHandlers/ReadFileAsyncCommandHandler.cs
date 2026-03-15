using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.Application.Contracts.Services;
using SheetRecordDuplicator.Common.Results;

namespace SheetRecordDuplicator.Application.CommandHandlers;

public class ReadFileAsyncCommandHandler(
    ITextFileReader _textFileReader,
    ILogger<ReadFileAsyncCommandHandler> _logger) 
    : IGlobalCommandHandler<ReadFileAsyncCommand, Task<Result<string>>>
{
    public async Task<Result<string>> Handle(
        ReadFileAsyncCommand command, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Read File Attempt: {command.FullName}");
        try
        {
            return Result.Success(
                await _textFileReader.ReadToEndAsync(
                    command.FullName, 
                    cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType().ToString()} {ex.Message}");
            return Result.Failure<string>(Error.Exception($"{ex.Message}"));
        }
    }
}
