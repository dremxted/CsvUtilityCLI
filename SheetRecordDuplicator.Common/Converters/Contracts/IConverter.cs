namespace Lang.Common.Converters.Contracts;

public interface IConverter<in TIn, TOut>
{
    TOut Convert(TIn convertee);
}