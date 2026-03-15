using System;

namespace SheetRecordDuplicator.Application.Contracts.Services;

public interface ICsvReader
{
    Task<IEnumerable<IDictionary<string, object>>> GetRowsAsync(
        string fullName,
        CancellationToken cancellationToken);
}
