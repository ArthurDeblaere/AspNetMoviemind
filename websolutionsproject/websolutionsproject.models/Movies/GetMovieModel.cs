using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;

namespace websolutionsproject.models.Movies
{
    public class GetMovieModel : BaseMovieModel
    {
        public Guid Id { get; set; }

        public ICollection<GetUserModel> Favorites { get; set; }
        public ICollection<GetReviewModel> Reviews { get; set; }
    }
}
