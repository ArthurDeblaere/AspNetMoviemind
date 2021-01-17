using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Movies;

namespace websolutionsproject.api.Repositories
{
    public interface IMovieRepository
    {
        Task<List<GetMovieModel>> GetMovies();
        Task<GetMovieModel> GetMovie(string id);
        Task<GetMovieModel> PostMovie(PostMovieModel postMovieModel);
        Task PutMovie(string id, PutMovieModel putMovieModel);
        Task DeleteMovie(string id);
        Task CalculateOverallRating(string id);
    }
}
