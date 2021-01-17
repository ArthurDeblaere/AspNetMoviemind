using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.shared.Exceptions
{
    public class EntityException : MovieMindException
    {
        public EntityException(
            string message,
            string sourceClass,
            string sourceMethod,
            string status) : base(message, sourceClass, sourceMethod, status)
        {
        }
    }
}
