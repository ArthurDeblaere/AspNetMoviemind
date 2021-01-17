using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.api.Entities
{
    public class Role : IdentityRole<Guid>
    {
        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
