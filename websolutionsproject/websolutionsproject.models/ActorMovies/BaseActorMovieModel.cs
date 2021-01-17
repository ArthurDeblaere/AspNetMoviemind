using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.ActorMovies
{
    public class BaseActorMovieModel
    {
        [Required(ErrorMessage = "ActorId is required")]
        public Guid ActorId { get; set; }
        public GetActorModel Actor { get; set; }

        [Required(ErrorMessage = "Movieid")]
        public Guid MovieId { get; set; }
        public GetMovieModel Movie { get; set; }
    }
}
