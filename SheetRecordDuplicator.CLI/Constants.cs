namespace SheetRecordDuplicator.CLI;

public static class Constants
{
    //Arguments
    public const string TemplateFileArgument = "template";
    public const string InputsFileArgument = "inputs";

    //Options
    public const string OutputOption = "--output";
    public const string OutputOptionAlias = "-o";

    //Defaults
    public static string DefaultOutputFileName => $"CSV-Output-{DateTime.Now.ToString("MMddyyyHHmmss")}.csv";
}
