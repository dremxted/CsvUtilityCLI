using SheetRecordDuplicator.Application.Commands;
using SheetRecordDuplicator.Application.Contracts.Commands;

namespace SheetRecordDuplicator.Application.CommandHandlers;

public class CombineStringCommandHandler : IGlobalCommandHandler<CombineStringsCommand, IEnumerable<string>>
{
    public IEnumerable<string> Handle(CombineStringsCommand command, CancellationToken cancellationToken)
    {
        foreach (var row in command.Rows)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string result = command.Template;
            foreach (var pair in row)
            {
                result = result.Replace(
                    oldValue: pair.Key.ToString(),
                    newValue: pair.Value.ToString());
            }
            yield return result;
        }
    }
}
