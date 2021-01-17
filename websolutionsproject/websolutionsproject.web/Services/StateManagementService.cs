using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using websolutionsproject.models.Users;
using websolutionsproject.web.Models;

namespace websolutionsproject.web.Services
{
    public class StateManagementService : IStateManagementService
    {
        public const string SessionKeyId = "_Id";
        public const string SessionKeyFirstName = "_FirstName";
        public const string SessionKeyLastName = "_LastName";
        public const string SessionKeyUserName = "_UserName";
        public const string SessionKeyJwtToken = "_JwtToken";
        public const string SessionKeyJwtExpiresOn = "_JwtExpiresOn";
        public const string SessionKeyRefreshToken = "_RefreshToken";
        public const string SessionKeyRtExpiresOn = "_RtExpiresOn";
        public const string SessionKeyRoles = "_Roles";

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSecurityTokenHandler handler;

        public StateManagementService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            handler = new JwtSecurityTokenHandler();
        }

        public void SetSession(string stateManagementModelJson)
        {
            StateManagementModel stateManagementModel = JsonSerializer.Deserialize<StateManagementModel>(stateManagementModelJson);

            SetSessionData(stateManagementModel);
        }

        public void SetState(PostAuthenticateResponseModel postAuthenticateResponseModel)
        {
            string refreshToken = postAuthenticateResponseModel.RefreshToken.Split(';').Single(x => x.Contains("MovieMind.RefreshToken=")).Substring(24);
            string rtExpiresOn = postAuthenticateResponseModel.RefreshToken.Split(';').Single(x => x.Contains("expires=")).Substring(9);

            StateManagementModel stateManagementModel = new StateManagementModel
            {
                Id = postAuthenticateResponseModel.Id,
                FirstName = postAuthenticateResponseModel.FirstName,
                LastName = postAuthenticateResponseModel.LastName,
                UserName = postAuthenticateResponseModel.UserName,
                JwtToken = postAuthenticateResponseModel.JwtToken,
                JwtExpiresOn = handler.ReadJwtToken(postAuthenticateResponseModel.JwtToken).ValidTo,
                RefreshToken = refreshToken,
                RtExpiresOn = Convert.ToDateTime(rtExpiresOn).ToUniversalTime(),
                Roles = postAuthenticateResponseModel.Roles
            };

            SetSessionData(stateManagementModel);
        }

        public void ClearState()
        {
            _httpContextAccessor.HttpContext.Session.Clear();      
        }


        private void SetSessionData(StateManagementModel stateManagementModel)
        {
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyId, stateManagementModel.Id.ToString());
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyFirstName, stateManagementModel.FirstName);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyLastName, stateManagementModel.LastName);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyUserName, stateManagementModel.UserName);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyJwtToken, stateManagementModel.JwtToken);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyJwtExpiresOn, handler.ReadJwtToken(stateManagementModel.JwtToken).ValidTo.ToString());
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyRefreshToken, stateManagementModel.RefreshToken);
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyRtExpiresOn, stateManagementModel.RtExpiresOn.ToString());
            _httpContextAccessor.HttpContext.Session.SetString(SessionKeyRoles, string.Join(",", stateManagementModel.Roles));
        }
    }
}
