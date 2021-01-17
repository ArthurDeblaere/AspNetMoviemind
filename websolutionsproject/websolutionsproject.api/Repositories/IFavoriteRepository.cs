using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Favorites;

namespace websolutionsproject.api.Repositories
{
    public interface IFavoriteRepository
    {
        Task<GetFavoriteModel> GetFavorite(string id);
        Task<List<GetFavoriteModel>> GetFavorites();
        Task<GetFavoriteModel> PostFavorite(PostFavoriteModel postFavoriteModel);
        Task PutFavorite(string id, PutFavoriteModel putFavoriteModel);
        Task DeleteFavorite(string id);
    }
}
