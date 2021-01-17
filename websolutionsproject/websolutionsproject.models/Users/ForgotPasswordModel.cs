using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace websolutionsproject.models.Users
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}