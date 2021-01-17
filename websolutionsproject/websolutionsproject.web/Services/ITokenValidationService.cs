using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Services
{
    public interface ITokenValidationService
    {
        Task Validate(string sourceClass, string sourceMethod);
    }
}
