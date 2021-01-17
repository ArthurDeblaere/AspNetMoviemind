using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class MovieMindError
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string SourceClass { get; set; }
        public string SourceMethod { get; set; }
        public string Status { get; set; }
    }
}
