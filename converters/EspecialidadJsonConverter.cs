using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Models;

public class EspecialidadJsonConverter : JsonConverter<Especialidad>
{
	public override Especialidad Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var strValue = reader.GetString();
		return (Especialidad)Enum.Parse(typeof(Especialidad), strValue);
	}

	public override void Write(Utf8JsonWriter writer, Especialidad value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}
