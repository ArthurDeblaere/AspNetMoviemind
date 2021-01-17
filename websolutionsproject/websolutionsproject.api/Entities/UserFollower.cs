using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class UserFollower
    {
        public Guid Id { get; set; }
        public Guid FollowingId { get; set; }
        public virtual User Following { get; set; }
        public Guid FollowerId { get; set; }
        public virtual User Follower { get; set; }
    }
}
