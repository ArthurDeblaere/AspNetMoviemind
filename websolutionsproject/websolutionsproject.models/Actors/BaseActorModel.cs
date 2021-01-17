using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.Actors
{
    public class BaseActorModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.DateTime)]
        public DateTime Birth { get; set; }

        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "Nationality is required")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<GetMovieModel> Movies { get; set; }

        [Display(Name = "Movies where this actor plays in")]
        public ICollection<Guid> MovieIds { get; set; }

    }
}
