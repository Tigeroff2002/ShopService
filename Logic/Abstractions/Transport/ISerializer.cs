namespace Logic.Abstractions.Transport;

public interface ISerializer<TSource, TTarget>
{
    TTarget Serialize(TSource source);
}
