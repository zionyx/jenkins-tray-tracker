using System;
using System.Text;
using JenkinsTray.Entities;
using Newtonsoft.Json;

namespace JenkinsTray.Utils.Serialization
{
    internal class CredentialsJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Credentials);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType != JsonToken.StartArray)
                throw new Exception("Expecting StartArray, received " + reader.TokenType);

            reader.Read();
            var username = (string) reader.Value;
            reader.Read();
            var passwordBase64 = (string) reader.Value;
            reader.Read();

            var password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordBase64));
            var res = new Credentials(username, password);
            return res;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var credentials = (Credentials) value;
            var passwordBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials.Password));
            writer.WriteStartArray();
            writer.WriteValue(credentials.Username);
            writer.WriteValue(passwordBase64);
            writer.WriteEndArray();
        }
    }
}