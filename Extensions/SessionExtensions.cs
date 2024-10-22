// using Microsoft.AspNetCore.Http;
// using System.Text.Json; // Ensure this is the only JSON library referenced

// public static class SessionExtensions
// {
//     public static void Set<T>(this ISession session, string key, T value)
//     {
//         session.SetString(key, JsonSerializer.Serialize(value));
//     }

//     public static T? Get<T>(this ISession session, string key)
//     {
//         var value = session.GetString(key);
//         return value == null ? default : JsonSerializer.Deserialize<T>(value);
//     }
// }
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class SessionExtensions
{
    // Serialize an object and store it in session
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    // Deserialize a session value back into an object
    public static T? GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
    }
}
