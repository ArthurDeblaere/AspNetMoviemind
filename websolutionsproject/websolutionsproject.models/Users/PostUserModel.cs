using websolutionsproject.models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace websolutionsproject.models.Users
{
    public class PostUserModel : BaseUserModel
    {

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^a-zA-Z0-9].*).{6,}$", ErrorMessage = "Minimum 6 characters with at least one capital letter, small letter, number and special sign")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [JsonIgnore]
        [Display(Name = "Repeat password")]
        public string ConfirmPassword { get; set; }

    }
}
