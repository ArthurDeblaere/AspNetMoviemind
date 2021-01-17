using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class ActorMovie
    {
        public Guid Id { get; set; }
        public Guid ActorId { get; set; }
        public Actor Actor { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
