using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class User : IdentityUser<Guid>
    { 
        [Display(Name = "First Name")]
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        public string Description { get; set; }
        //public string Email { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Favorite> Favorites { get; set; }

        //https://stackoverflow.com/questions/39728016/self-referencing-many-to-many-relations
        public virtual ICollection<UserFollower> UserFollowers { get; set; }
        public virtual ICollection<UserFollower> UserFollowing { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }


    }
}
