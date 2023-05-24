using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Models;

public class RolJsonConverter : JsonConverter<Rol>
{
	public override Rol Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var strValue = reader.GetString();
		return (Rol)Enum.Parse(typeof(Rol), strValue);
	}

	public override void Write(Utf8JsonWriter writer, Rol value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
