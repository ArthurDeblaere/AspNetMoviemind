using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rating { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public Guid MovieId { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
