using Microsoft.Extensions.Logging;
using SheetRecordDuplicator.CLI.Commands.CLI;
using SheetRecordDuplicator.CLI.Contracts;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace SheetRecordDuplicator.CLI.Factories;

public class SheetRecordDuplicatorFactory(
    ILogger<SheetRecordDuplicatorFactory> _logger) 
    : ICommandFactory<ApplicationRootCommand>
{
    public IEnumerable<Argument> CreateArguments()
    {
        var templateArgument = new Argument<FileInfo>(Constants.TemplateFileArgument);
        var inputsArgument = new Argument<FileInfo>(Constants.InputsFileArgument);

        templateArgument.Description = "A FullName to the template file.";
        inputsArgument.Description = "A FullName to the inputs file.";

        templateArgument.Validators.AddRange(
            DoesNotExistArgumentValidator, 
            ExtensionValidator);

        inputsArgument.Validators.AddRange(
            DoesNotExistArgumentValidator, 
            ExtensionValidator);

        return [templateArgument, inputsArgument];
    }

    public IEnumerable<Option> CreateOptions()
    {
        var outputOption = new Option<FileInfo>(Constants.OutputOption, Constants.OutputOptionAlias);
        outputOption.Validators.Add(AlreadyExistsOptionValidator);
        outputOption.Description = "Sets the FullName of the output file.";
        outputOption.DefaultValueFactory = (argResult) => new FileInfo(Constants.DefaultOutputFileName);

        return [outputOption];
    }
    private void DoesNotExistArgumentValidator(ArgumentResult argumentResult)
    {
        string argument = argumentResult.Argument.Name;
        FileInfo? fileInfo;

        try
        {
            fileInfo = argumentResult.GetValue<FileInfo>(argument)!;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Handling the {ex.GetType().ToString()}: {ex.Message}");
            return;
        }

        if(!fileInfo.Exists)
        {
            argumentResult.AddError($"A file with a given name does not exist");
        }
    }

    private void AlreadyExistsOptionValidator(OptionResult optionResult)
    {
        string argument = optionResult.Option.Name;
        FileInfo? fileInfo;

        try
        {
            fileInfo = optionResult.GetValue<FileInfo>(argument)!;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Handling the {ex.GetType().ToString()}: {ex.Message}");
            return;
        }

        if (fileInfo.Exists)
        {
            optionResult.AddError($"A file with a given name already exists.");
        }
    }

    private void ExtensionValidator(ArgumentResult argumentResult)
    {
        string[] extensions = [".csv", ".txt"];
        string argument = argumentResult.Argument.Name;
        FileInfo? fileInfo;

        try
        {
            fileInfo = argumentResult.GetValue<FileInfo>(argument)!;
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"Handling the {ex.GetType().ToString()}: {ex.Message}");
            return;
        }

        bool isValidExtension = extensions.Contains(fileInfo.Extension);
        if(!isValidExtension)
        {
            argumentResult.AddError($"The '{fileInfo.Extension}' extension cannot be read.");
        }
    }
}