using System.Text;

namespace SheetRecordDuplicator.Application.Contracts.Services;

public interface ITextFileWriter
{
Task WriteLineAsync(
        FileInfo fileInfo, 
        StringBuilder stringBuilder, 
        CancellationToken cancellationToken);
}
