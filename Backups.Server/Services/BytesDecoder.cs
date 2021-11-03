using System.Linq;
using System.Text;
using System.Text.Json;

namespace Backups.Server
{
    public class BytesDecoder : IBytesDecoder
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public BytesDecoder(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions;
        }
        
        public BytesData Encode<TData>(TData obj)
        {
            string serialized = JsonSerializer.Serialize(obj, _serializerOptions);
            return new BytesData(Encoding.Default.GetBytes(serialized));
        }

        public TData Decode<TData>(BytesData data)
        {
            string serialized = Encoding.Default.GetString(data.Bytes);
            return JsonSerializer.Deserialize<TData>(serialized);
        }
    }
}