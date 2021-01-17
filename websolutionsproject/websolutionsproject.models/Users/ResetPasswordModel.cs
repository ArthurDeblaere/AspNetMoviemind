using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace websolutionsproject.models.Users
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*\d.*)(?=.*[a-z].*)(?=.*[A-Z].*)(?=.*[^a-zA-Z0-9].*).{6,}$", ErrorMessage = "Minimum 6 characters with at least one capital letter, small letter, number and special sign")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Repeat password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
