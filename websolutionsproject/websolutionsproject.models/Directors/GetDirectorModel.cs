using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;

namespace websolutionsproject.models.Directors
{
    public class GetDirectorModel : BaseDirectorModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
