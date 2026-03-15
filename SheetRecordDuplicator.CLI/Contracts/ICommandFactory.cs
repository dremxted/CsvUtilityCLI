using System.CommandLine;

namespace SheetRecordDuplicator.CLI.Contracts;
public interface ICommandFactory<T>
{
    IEnumerable<Argument> CreateArguments();
    IEnumerable<Option> CreateOptions();
}