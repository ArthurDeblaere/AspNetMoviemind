using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace websolutionsproject.shared.Exceptions
{
    [JsonConverter(typeof(MovieMindExceptionConverter))]
    public class MovieMindException : Exception
    {
        public MovieMindError MovieMindError { get; }

        public MovieMindException(string message, string sourceClass, string sourceMethod, string status) : base(message)
        {
            MovieMindError = new MovieMindError
            {
                Type = this.GetType().Name,
                Message = message,
                SourceClass = sourceClass,
                SourceMethod = sourceMethod,
                Status = status
            };
        }

        public MovieMindException(string type, string message, string sourceClass, string sourceMethod, string status) : base(message)
        {
            MovieMindError = new MovieMindError
            {
                Type = type,
                Message = message,
                SourceClass = sourceClass,
                SourceMethod = sourceMethod,
                Status = status
            };
        }
    }
}
