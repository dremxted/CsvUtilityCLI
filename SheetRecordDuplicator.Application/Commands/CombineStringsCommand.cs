namespace SheetRecordDuplicator.Application.Commands;

public record CombineStringsCommand(
    string Template, 
    IEnumerable<IDictionary<string, object>> Rows);