namespace SheetRecordDuplicator.Application.Contracts.Services;

public interface IStringCombinator
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="template"></param>
    /// <param name="inputs"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A collection of the <paramref name="template"/>, 
    /// where all cell values of an iterated row from the <paramref name="inputs"/> 
    /// are passed as arguments to the <paramref name="template"/>
    /// </returns>
    IEnumerable<string> Combine(
        string template,
        IEnumerable<IDictionary<string, object>> rows,
        CancellationToken cancellationToken);
}
