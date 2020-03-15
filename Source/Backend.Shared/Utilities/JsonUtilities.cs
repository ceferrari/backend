using System.Text.Json;

namespace Backend.Shared.Utilities
{
    public static class JsonUtilities
    {
        #region Serialize

        public static string Serialize(object obj)
        {
            return Serialize(obj, false, JsonNamingPolicy.CamelCase);
        }

        public static string Serialize(object obj, bool indent)
        {
            return Serialize(obj, indent, JsonNamingPolicy.CamelCase);
        }

        public static string Serialize(object obj, JsonNamingPolicy jsonNamingPolicy)
        {
            return Serialize(obj, false, jsonNamingPolicy);
        }

        public static string Serialize(object obj, bool indent, JsonNamingPolicy jsonNamingPolicy)
        {
            return JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                PropertyNamingPolicy = jsonNamingPolicy,
                WriteIndented = indent
            });
        }

        #endregion

        #region Deserialize

        public static object Deserialize(string json)
        {
            return Deserialize(json, true);
        }

        public static object Deserialize(string json, bool propertyNameCaseInsensitive)
        {
            return JsonSerializer.Deserialize(json, typeof(object), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = propertyNameCaseInsensitive
            });
        }

        public static TValue Deserialize<TValue>(string json)
        {
            return Deserialize<TValue>(json, true);
        }

        public static TValue Deserialize<TValue>(string json, bool propertyNameCaseInsensitive)
        {
            return JsonSerializer.Deserialize<TValue>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = propertyNameCaseInsensitive
            });
        }

        #endregion
    }
}
