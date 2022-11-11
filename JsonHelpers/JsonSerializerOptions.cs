using System.Text.Json;
using TinyHelpers.Json.Serialization;

namespace JsonHelpers;

public static class JsonSerializerOptionsClass
{
    public static JsonSerializerOptions JsonOptions()
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new TimeOnlyConverter());
        options.Converters.Add(new DateOnlyConverter());
        return options;
    }
}