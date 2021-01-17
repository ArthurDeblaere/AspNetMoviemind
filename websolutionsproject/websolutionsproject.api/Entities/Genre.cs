using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Genre
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength =2)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
