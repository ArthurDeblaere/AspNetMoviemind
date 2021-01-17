using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.Directors
{
    public class BaseDirectorModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required" )]
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

        public ICollection<GetMovieModel> Movies { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
