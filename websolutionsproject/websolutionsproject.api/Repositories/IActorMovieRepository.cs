using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.ActorMovies;

namespace websolutionsproject.api.Repositories
{
    public interface IActorMovieRepository
    {
        Task<List<GetActorMovieModel>> GetActorMovies();
        Task<GetActorMovieModel> GetActorMovie(string id);
        Task<GetActorMovieModel> PostActorMovie(PostActorMovieModel postActorMovieModel);
        Task PutActorMovie(string id, PutActorMovieModel putActorMovieModel);
        Task DeleteActorMovie(string id);

    }
}
