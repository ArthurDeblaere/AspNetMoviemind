using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class GuidException : MovieMindException
    {
        public GuidException(
            string message,
            string sourceClass,
            string sourceMethod,
            string status) : base(message, sourceClass, sourceMethod, status)
        {
        }
    }
}
