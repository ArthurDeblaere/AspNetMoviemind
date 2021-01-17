using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Roles;

namespace websolutionsproject.api.Repositories
{
    public interface IRoleRepository
    {
        Task<List<GetRoleModel>> GetRoles();
        Task<GetRoleModel> GetRole(string id);
        Task PutRole(string id, PutRoleModel putRoleModel);
    }
}
