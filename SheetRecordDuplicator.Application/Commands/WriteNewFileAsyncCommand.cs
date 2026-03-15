using System.Text;

namespace SheetRecordDuplicator.Application.Commands;

public record WriteNewFileAsyncCommand(FileInfo FileInfo, StringBuilder StringBuilder);