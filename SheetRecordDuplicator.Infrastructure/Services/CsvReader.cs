using MiniExcelLibs;
using SheetRecordDuplicator.Application.Contracts.Services;

namespace SheetRecordDuplicator.Infrastructure.Services;

public class CsvReader : ICsvReader
{
    public async Task<IEnumerable<IDictionary<string, object>>> GetRowsAsync(
        string FullName,
        CancellationToken cancellationToken)
    {
        IEnumerable<object> queryResult = await MiniExcel.QueryAsync(
            path: FullName, 
            useHeaderRow: true,
            excelType: ExcelType.CSV,
            cancellationToken: cancellationToken);

        return queryResult.Cast<IDictionary<string, object>>();
    }

}