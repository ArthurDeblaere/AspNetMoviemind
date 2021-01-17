using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Models
{
    public class StateManagementModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public DateTime JwtExpiresOn { get; set; }
        public bool JwtIsExpired => DateTime.UtcNow >= JwtExpiresOn;
        public string RefreshToken { get; set; }
        public DateTime RtExpiresOn { get; set; }
        public bool RtIsExpired => DateTime.UtcNow >= RtExpiresOn;
        public ICollection<string> Roles { get; set; }
        //public AfbeeldingModel AfbeeldingModel { get; set; }
    }
}
