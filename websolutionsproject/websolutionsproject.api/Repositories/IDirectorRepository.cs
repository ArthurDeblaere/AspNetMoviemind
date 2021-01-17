using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Directors;

namespace websolutionsproject.api.Repositories
{
    public interface IDirectorRepository
    {
        Task<List<GetDirectorModel>> GetDirectors();
        Task<GetDirectorModel> GetDirector(string id);
        Task<GetDirectorModel> PostDirector(PostDirectorModel postDirectorModel);
        Task PutDirector(string id, PutDirectorModel putDirectorModel);
        Task DeleteDirector(string id);
    }
}
