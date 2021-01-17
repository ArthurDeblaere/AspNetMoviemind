using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Helpers
{
    public class LocalizerHelper
    {
        private readonly IStringLocalizer<LocalizerHelper> _localizer;

        public LocalizerHelper(IStringLocalizerFactory factory)
        {
            _localizer = new StringLocalizer<LocalizerHelper>(factory);
        }

        public string LocalizeString(string message)
        {
            return _localizer[message];
        }
    }
}
