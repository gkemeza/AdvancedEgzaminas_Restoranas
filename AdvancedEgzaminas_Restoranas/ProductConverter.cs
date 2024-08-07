using AdvancedEgzaminas_Restoranas.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AdvancedEgzaminas_Restoranas
{
    public class ProductConverter : JsonConverter<Product>
    {
        public override Product Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var rootElement = doc.RootElement;

                if (rootElement.TryGetProperty("Type", out JsonElement typeElement))
                {
                    string type = typeElement.GetString();
                    switch (type)
                    {
                        case "Food":
                            return JsonSerializer.Deserialize<Food>(rootElement.GetRawText(), options);
                        case "Drink":
                            return JsonSerializer.Deserialize<Drink>(rootElement.GetRawText(), options);
                        default:
                            throw new NotSupportedException($"Type '{type}' is not supported");
                    }
                }

                throw new JsonException("Missing type discriminator");
            }
        }

        public override void Write(Utf8JsonWriter writer, Product value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}
