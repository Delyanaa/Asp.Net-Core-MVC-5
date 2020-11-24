using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetJson<T>(this ISession session, string key, T value)
        {
            var jsonObj = Encoding.ASCII.GetBytes(JsonSerializer.Serialize<T>(value));
            session.Set(key,jsonObj);
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            var deserializedCart = sessionData == null
              ? default(T) : JsonSerializer.Deserialize<T>(sessionData);

            return deserializedCart;
        }
    }
}