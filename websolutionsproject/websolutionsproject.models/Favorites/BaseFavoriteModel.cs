using System;
using System.Collections.Generic;
using System.Text;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Users;

namespace websolutionsproject.models.Favorites
{
    public class BaseFavoriteModel
    {
        public Guid UserId { get; set; }
        public GetUserModel User { get; set; }
        public Guid MovieId { get; set; }
        public GetMovieModel Movie { get; set; }
    }
}
