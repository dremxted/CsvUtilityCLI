using Microsoft.Extensions.DependencyInjection;
using SheetRecordDuplicator.Application.Contracts.Commands;

namespace SheetRecordDuplicator.Application.Services;

public class GlobalCommandDispatcher(IServiceProvider serviceProvider) : IGlobalCommandDispatcher
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public TCommandResult Dispatch<TCommand, TCommandResult>(
        TCommand command,
        CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService<IGlobalCommandHandler<TCommand, TCommandResult>>();
        return handler.Handle(command, cancellationToken);
    }
}
