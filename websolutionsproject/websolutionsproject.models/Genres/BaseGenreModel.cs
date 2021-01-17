using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace websolutionsproject.models.Genres
{
    public class BaseGenreModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(30, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
