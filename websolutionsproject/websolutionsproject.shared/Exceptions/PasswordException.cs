using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class PasswordException : MovieMindException
    {
        public PasswordException(
           string message,
           string sourceClass,
           string sourceMethod,
           string status) : base(message, sourceClass, sourceMethod, status)
        {
        }
    }
}
