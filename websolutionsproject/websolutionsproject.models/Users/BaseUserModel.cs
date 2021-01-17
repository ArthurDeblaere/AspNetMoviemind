using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Roles;
using websolutionsproject.models.Users;

namespace websolutionsproject.models.Users
{
    public class BaseUserModel
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public ICollection<GetReviewModel> Reviews { get; set; }
        public ICollection<GetMovieModel> Favorites { get; set; }

        public virtual ICollection<GetUserModel> Followers { get; set; }
        //public virtual ICollection<GetUserModel> Followings { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
