using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Actors;

namespace websolutionsproject.api.Repositories
{
    public interface IActorRepository
    {
        Task<List<GetActorModel>> GetActors();
        Task<GetActorModel> GetActor(string id);
        Task<GetActorModel> PostActor(PostActorModel postActorModel);
        Task PutActor(string id, PutActorModel putActorModel);
        Task DeleteActor(string id);
    }
}
