using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using JenkinsTray.Entities;

namespace JenkinsTray.Utils.Serialization
{
    class CredentialsJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Credentials);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType != JsonToken.StartArray)
                throw new Exception("Expecting StartArray, received " + reader.TokenType);

            reader.Read();
            string username = (string)reader.Value;
            reader.Read();
            string passwordBase64 = (string)reader.Value;
            reader.Read();

            string password = Encoding.UTF8.GetString(Convert.FromBase64String(passwordBase64));
            Credentials res = new Credentials(username, password);
            return res;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Credentials credentials = (Credentials)value;
            string passwordBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials.Password));
            writer.WriteStartArray();
            writer.WriteValue(credentials.Username);
            writer.WriteValue(passwordBase64);
            writer.WriteEndArray();
        }
    }
}
