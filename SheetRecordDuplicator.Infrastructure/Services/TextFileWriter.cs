using System.Text;
using SheetRecordDuplicator.Application.Contracts.Services;

namespace SheetRecordDuplicator.Infrastructure.Services;

public class TextFileWriter : ITextFileWriter
{
    public async Task WriteLineAsync(
        FileInfo fileInfo, 
        StringBuilder stringBuilder, 
        CancellationToken cancellationToken)
    {
        FileStream fileStream = new(
            path: fileInfo.FullName, 
            FileMode.CreateNew, // IO exception is thrown if file exists
            FileAccess.Write, 
            FileShare.ReadWrite);

        using (StreamWriter streamWriter = new(fileStream))
        {
            await streamWriter.WriteAsync(
                value: stringBuilder, 
                cancellationToken: cancellationToken);
            streamWriter.Close();
        }
    }
}
