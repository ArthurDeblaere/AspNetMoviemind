using System;
using System.Collections.Generic;
using System.Text;
using websolutionsproject.models.Users;

namespace websolutionsproject.models.UserFollowers
{
    public class BaseUserFollowerModel
    {
        public Guid FollowingId { get; set; }
        public virtual GetUserModel Following { get; set; }
        public Guid FollowerId { get; set; }
        public virtual GetUserModel Follower { get; set; }
    }
}
