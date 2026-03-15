namespace SheetRecordDuplicator.Common.Results;

public sealed record Error
{
    private readonly object[] _extensions;

    public readonly string Code;
    public readonly string Message;
    public readonly ErrorType? Type;

    public IReadOnlyCollection<object> Extensions => _extensions;
    public static readonly Error None = new Error(ErrorType.None, string.Empty, string.Empty, []);

    public Error(ErrorType errorType, string code, string message, params object[] extensions)
    {
        Type = errorType;
        Code = code;
        Message = message;
        _extensions = extensions;
    }

    public static Error Exception(string message, params object[] extensions) => 
        new Error(ErrorType.Exception, "Exception", message, extensions);

}

public enum ErrorType
{
    None = 0,
    Exception = 1,
    Failure = 2,
    Validation = 3,
    NotFound = 4,
    Conflict = 5
}