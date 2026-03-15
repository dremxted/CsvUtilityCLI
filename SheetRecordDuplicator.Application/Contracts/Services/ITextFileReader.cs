namespace SheetRecordDuplicator.Application.Contracts.Services;

public interface ITextFileReader
{
    Task<string> ReadToEndAsync(string FullName, CancellationToken cancellationToken);
}
