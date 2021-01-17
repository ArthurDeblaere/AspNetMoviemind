using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Genres;

namespace websolutionsproject.api.Repositories
{
    public interface IGenreRepository
    {
        Task<List<GetGenreModel>> GetGenres();
        Task<GetGenreModel> GetGenre(string id);
        Task<GetGenreModel> PostGenre(PostGenreModel postGenreModel);
        Task PutGenre(string id, PutGenreModel putGenreModel);
        Task DeleteGenre(string id);
    }
}
