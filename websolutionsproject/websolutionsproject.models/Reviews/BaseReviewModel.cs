using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Users;

namespace websolutionsproject.models.Reviews
{
    public class BaseReviewModel
    {
        [Required(ErrorMessage = "Description is required" )]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public GetUserModel User { get; set; }

        [Display(Name = "Movie")]
        [Required(ErrorMessage = "Movie is required")]
        public Guid MovieId { get; set; }
        public GetMovieModel Movie { get; set; }
    }
}
