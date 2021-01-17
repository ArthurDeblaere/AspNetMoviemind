using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.Genres
{
    public class GetGenreModel : BaseGenreModel
    {
        [Required]
        public Guid Id { get; set; }

        public ICollection<GetMovieModel> Movies { get; set; }
    }
}
