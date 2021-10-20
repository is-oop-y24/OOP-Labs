using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backups.Server.Tools
{
    public class ServerExceptionConverter : JsonConverter<ServerException>
    {
        public override ServerException Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            reader.Read();
            reader.Read();
            var exception = new ServerException(reader.GetString());
            reader.Read();
            return exception;
        }

        public override void Write(Utf8JsonWriter writer, ServerException value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Message", value.Message);
            writer.WriteEndObject();
        }
    }
}