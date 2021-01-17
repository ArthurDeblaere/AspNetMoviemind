using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.RefreshTokens;
using websolutionsproject.models.Users;

namespace websolutionsproject.api.Repositories
{
    public interface IUserRepository
    {
        Task<List<GetUserModel>> GetUsers();
        Task<GetUserModel> GetUser(string id);
        Task<GetUserModel> PostUser(PostUserModel postUserModel);
        Task PutUser(string id, PutUserModel putUserModel);
        Task PatchUser(string id, PatchUserModel patchUserModel);
        Task DeleteUser(string id);
        Task<ResetPasswordModel> CreateResetToken(ForgotPasswordModel forgotPasswordModel);
        Task ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<List<GetRefreshTokenModel>> GetUserRefreshTokens(Guid id);

        // JWT methods
        Task<PostAuthenticateResponseModel> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel, string ipAddress);
        Task<PostAuthenticateResponseModel> RefreshToken(string token, string ipAddress);
        Task RevokeToken(string token, string ipAddress);
    }
}
