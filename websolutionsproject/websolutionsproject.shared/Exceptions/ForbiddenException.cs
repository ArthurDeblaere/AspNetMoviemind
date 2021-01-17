using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class ForbiddenException : MovieMindException
    {
        public ForbiddenException(
               string message,
               string sourceClass,
               string sourceMethod,
               string status) : base(message, sourceClass, sourceMethod, status)
        {
        }
    }
}
