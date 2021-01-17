using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
