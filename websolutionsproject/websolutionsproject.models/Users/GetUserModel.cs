using websolutionsproject.models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace websolutionsproject.models.Users
{
    public class GetUserModel : BaseUserModel
    {
        public Guid Id { get; set; }

        //public ICollection<string> Roles { get; set; }
    }
}
