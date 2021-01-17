using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Genres;

namespace websolutionsproject.models.Movies
{
    public class BaseMovieModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Range(0, 3000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Average rating")]
        public int OverallRating { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration in minutes")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Director is required")]
        [Display(Name = "Director")]
        public Guid DirectorId { get; set; }
        public GetDirectorModel Director { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }
        public GetGenreModel Genre {get; set;}

        public ICollection<GetActorModel> Actors { get; set; }

        [Display(Name = "Actors starring in this movie")]
        public ICollection<Guid> ActorIds { get; set; }
    }
}
