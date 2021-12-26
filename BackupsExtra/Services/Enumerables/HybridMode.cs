using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackupsExtra.Services.Enumerables
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HybridMode
    {
        And,
        Or,
    }
}