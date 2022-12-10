using Logic.Abstractions.Transport;
using Newtonsoft.Json;

namespace Logic.Transport;

public sealed class Deserializer<TTarget>
    : IDeserializer<string, TTarget>
{
    public TTarget Deserialize(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            throw new ArgumentException(source);
        }

        var message =  JsonConvert.DeserializeObject<TTarget>(source);

        ArgumentNullException.ThrowIfNull(message);

        return message;
    }
}
