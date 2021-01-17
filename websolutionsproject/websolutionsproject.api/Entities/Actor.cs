using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Actor
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name ="First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Birth { get; set; }

        public string Nationality { get; set; }

        public string Description { get; set; }

        public ICollection<ActorMovie> ActorMovies { get; set; }
    }
}
