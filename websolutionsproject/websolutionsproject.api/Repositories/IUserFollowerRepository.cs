using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.UserFollowers;

namespace websolutionsproject.api.Repositories
{
    public interface IUserFollowerRepository
    {
        Task<GetUserFollowerModel> GetUserFollower(string id);
        Task<List<GetUserFollowerModel>> GetUserFollowers();
        Task<GetUserFollowerModel> PostUserFollower(PostUserFollowerModel postUserFollowerModel);
        Task DeleteUserFollower(string id);
    }
}
