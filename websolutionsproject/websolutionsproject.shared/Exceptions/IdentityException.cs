using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class IdentityException : MovieMindException
    {
        public IdentityException(
              string message,
              string sourceClass,
              string sourceMethod,
              string status) : base(message, sourceClass, sourceMethod, status)
        {
        }
    }
}
