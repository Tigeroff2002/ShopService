using Logic.Abstractions.Transport;
using Newtonsoft.Json;

namespace Logic.Transport;

public sealed class Serializer<TSource>
    : ISerializer<TSource, string>
{
    public string Serialize(TSource source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return JsonConvert.SerializeObject(source);
    }
}
