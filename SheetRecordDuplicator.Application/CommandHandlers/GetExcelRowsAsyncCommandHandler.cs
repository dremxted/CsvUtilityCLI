using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;
using SheetRecordDuplicator.Application.Contracts.Services;
using SheetRecordDuplicator.Common.Results;

namespace SheetRecordDuplicator.Application.CommandHandlers;

public class GetExcelRowsAsyncCommandHandler(
    ICsvReader _csvReader,
    ILogger<GetExcelRowsAsyncCommand> _logger)
    : IGlobalCommandHandler<
        GetExcelRowsAsyncCommand, 
        Task<Result<IEnumerable<IDictionary<string, object>>>>>
{
    public async Task<Result<IEnumerable<IDictionary<string, object>>>> Handle(
        GetExcelRowsAsyncCommand command, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Read File Attempt: {command.FullName}");
        try 
        {
            return Result.Success(
                await _csvReader.GetRowsAsync(
                    command.FullName,
                    cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ex.GetType().ToString()} {ex.Message}");
            return Result.Failure<IEnumerable<IDictionary<string, object>>>(
                Error.Exception($"{ex.Message}"));
        }
    }
}
