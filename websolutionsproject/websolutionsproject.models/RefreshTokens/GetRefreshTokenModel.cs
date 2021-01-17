using System;
using System.Collections.Generic;
using System.Text;

namespace websolutionsproject.models.RefreshTokens
{
    public class GetRefreshTokenModel
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive { get; set; }
    }
}
