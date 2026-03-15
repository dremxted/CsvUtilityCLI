using SheetRecordDuplicator.Application.Contracts.Services;

namespace SheetRecordDuplicator.Infrastructure.Repositories;

public class TextFileReader : ITextFileReader
{
    public async Task<string> ReadToEndAsync(string FullName, CancellationToken cancellationToken)
    {
        string csvOutput = string.Empty;
        FileStream fileStream = new(
            path: FullName, 
            FileMode.Open, 
            FileAccess.Read, 
            FileShare.ReadWrite);

        using (StreamReader streamReader = new(fileStream))
        {
            csvOutput = await streamReader.ReadToEndAsync(cancellationToken);
            streamReader.Close();
        }

        return csvOutput;
    }
}