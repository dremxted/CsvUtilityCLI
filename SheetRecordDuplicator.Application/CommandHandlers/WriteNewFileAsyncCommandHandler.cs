using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.Application.Contracts.Services;
using SheetRecordDuplicator.Common.Results;

namespace SheetRecordDuplicator.Application.CommandHandlers;

public class WriteNewFileAsyncCommandHandler(
    ITextFileWriter _textFileWriter,
    ILogger<WriteNewFileAsyncCommandHandler> _logger) 
    : IGlobalCommandHandler<WriteNewFileAsyncCommand, Task<Result>>
{
    public async Task<Result> Handle(
        WriteNewFileAsyncCommand command, 
        CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Create File Attempt: {command.FileInfo.Name}");
            await _textFileWriter.WriteLineAsync(
                command.FileInfo,
                command.StringBuilder,
                cancellationToken);

            _logger.LogInformation($"A new File has been created: {command.FileInfo.Name}");
        }
        catch(Exception ex)
        {
            _logger.LogWarning($"Handling the {ex.GetType().ToString()}: {ex.Message}");
            return Result.Failure(Error.Exception($"{ex.Message}"));
        }
        return Result.Success();
    }
}