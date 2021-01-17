using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace websolutionsproject.shared.Exceptions
{
    public class MovieMindExceptionConverter : JsonConverter<MovieMindException>
    {
        public override MovieMindException Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string type = string.Empty;
            string message = string.Empty;
            string sourceClass = string.Empty;
            string sourceMethod = string.Empty;
            string status = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                reader.Read();

                if (i == 1)
                {
                    type = reader.GetString();
                }

                if (i == 3)
                {
                    message = reader.GetString();
                }

                if (i == 5)
                {
                    sourceClass = reader.GetString();
                }

                if (i == 7)
                {
                    sourceMethod = reader.GetString();
                }

                if (i == 9)
                {
                    status = reader.GetString();
                }
            }

            reader.Read();

            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new MovieMindException(type, message, sourceClass, sourceMethod, status);
            }
            else
            {
                throw new MovieMindException("Please provide valid input", "MovieMindExceptionConverter", "MovieMindExceptionRead", "500");
            }
        }

        public override void Write(Utf8JsonWriter writer, MovieMindException value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
