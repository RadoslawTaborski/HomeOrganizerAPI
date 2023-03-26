﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeOrganizerAPI.Helpers;

public class IntToStringConventer : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        var success = int.TryParse(str, out int value);
        return success ? value : 0;
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToString());
    }
}
