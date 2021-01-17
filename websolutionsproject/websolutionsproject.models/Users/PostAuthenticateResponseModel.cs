using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.models.Users
{
    public class PostAuthenticateResponseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public ICollection<string> Roles { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
