namespace SheetRecordDuplicator.Application.Contracts.Commands;

public interface IGlobalCommandHandler<in TCommand, TCommandResult>
{
    TCommandResult Handle(TCommand command, CancellationToken cancellationToken);
}
