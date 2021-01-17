using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Users;

namespace websolutionsproject.web.Services
{
    public interface IStateManagementService
    {
        void SetSession(string postAuthenticateResponseModelJson);
        void SetState(PostAuthenticateResponseModel postAuthenticateResponseModel);
        void ClearState();
    }
}
