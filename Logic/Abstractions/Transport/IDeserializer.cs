namespace Logic.Abstractions.Transport;

public interface IDeserializer<TSource, TTarget>
{
    TTarget Deserialize(TSource source);
}
