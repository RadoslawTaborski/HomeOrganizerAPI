using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers;

public class GuidToStringConverter : JsonConverter<byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        var success = Guid.TryParse(str, out Guid value);
        return success ? value.ToByteArray() : null;
    }

    public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options) {
        if (value == null || value.Length!=16)
        {
            writer.WriteStringValue("");
            return;
        }
        writer.WriteStringValue(new Guid(value).ToString());
    }
}
