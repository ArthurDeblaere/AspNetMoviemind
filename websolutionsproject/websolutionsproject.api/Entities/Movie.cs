using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 3000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Year { get; set; }
        public string Description { get; set; }

        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int OverallRating { get; set; }

        [Required]
        public int Duration { get; set; }
        public Guid DirectorId { get; set; }
        public Director Director { get; set; }
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Review> Reviews { get; set; }

        //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
        public ICollection<ActorMovie> ActorMovies { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
    }
}
