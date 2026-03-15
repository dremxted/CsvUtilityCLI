namespace SheetRecordDuplicator.Application.Contracts.Commands;

public interface IGlobalCommandDispatcher
{
    TCommandResult Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken);
}
