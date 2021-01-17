using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace websolutionsproject.models.Roles
{
    public class BaseRoleModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
