using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.api.Helpers;
using websolutionsproject.models.Movies;
using websolutionsproject.models.RefreshTokens;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieMindContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;
        private readonly ClaimsPrincipal _user;

        public UserRepository(MovieMindContext context, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager, 
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.User;
        }

        #region crudmethods
        public async Task<List<GetUserModel>> GetUsers()
        {
            List<GetUserModel> users = await _context.Users
                .Include(x => x.UserRoles)
                .Select(x => new GetUserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Description = x.Description,
                    Email = x.Email,
                    UserName = x.UserName,
                    Roles = x.UserRoles.Select(x => x.Role.Name).ToList(),
                    Followers = (from follower in _context.UserFollowers
                                 where follower.FollowingId == x.Id
                                 select new GetUserModel
                                 {
                                     Id = follower.FollowerId,
                                     FirstName = follower.Follower.FirstName,
                                     LastName = follower.Follower.LastName,
                                     Description = follower.Follower.Description,
                                     Email = follower.Follower.Email
                                 }).ToList(),
                    Reviews = (from review in _context.Reviews
                               where review.UserId == x.Id
                               select new GetReviewModel
                               {
                                   Id = review.MovieId,
                                   Rating = review.Rating,
                                   Date = review.Date,
                                   Description = review.Description,
                                   UserId = review.UserId
                               }).ToList(),
                    Favorites = (from favorite in _context.Favorites
                                 where favorite.UserId == x.Id
                                 select new GetMovieModel
                                 {
                                     Id = favorite.MovieId,
                                     Name = favorite.Movie.Name,
                                     Year = favorite.Movie.Year,
                                     OverallRating = favorite.Movie.OverallRating,
                                     Duration = favorite.Movie.Duration,
                                     Description = favorite.Movie.Description,
                                     DirectorId = favorite.Movie.DirectorId,
                                     GenreId = favorite.Movie.GenreId
                                 }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<GetUserModel> GetUser(string id)
        {
            GetUserModel user = await _context.Users
                .Include(x => x.UserRoles)
                .Select(x => new GetUserModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    UserName = x.UserName,
                    Description = x.Description,
                    Roles = x.UserRoles.Select(x => x.Role.Name).ToList(),
                    Followers = (from follower in _context.UserFollowers
                                 where follower.FollowingId == x.Id
                                 select new GetUserModel
                                 {
                                     Id = follower.Id,
                                     FirstName = follower.Follower.FirstName,
                                     LastName = follower.Follower.LastName,
                                     Description = follower.Follower.Description,
                                     Email = follower.Follower.Email
                                 }).ToList(),
                    Reviews = (from review in _context.Reviews
                               where review.UserId == x.Id
                               select new GetReviewModel
                               {
                                   Id = review.Id,
                                   MovieId = review.MovieId,
                                   Rating = review.Rating,
                                   Date = review.Date,
                                   Description = review.Description,
                                   UserId = review.UserId
                               }).ToList(),
                    Favorites = (from favorite in _context.Favorites
                                 where favorite.UserId == x.Id
                                 select new GetMovieModel
                                 {
                                     Id = favorite.MovieId,
                                     Name = favorite.Movie.Name,
                                     Year = favorite.Movie.Year,
                                     OverallRating = favorite.Movie.OverallRating,
                                     Duration = favorite.Movie.Duration,
                                     Description = favorite.Movie.Description,
                                     DirectorId = favorite.Movie.DirectorId,
                                     GenreId = favorite.Movie.GenreId
                                 }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (user == null)
            {
                throw new EntityException("User not found", this.GetType().Name, "GetUser", "404");
            }

            return user;
        }

        public async Task<GetUserModel> PostUser(PostUserModel postUserModel)
        {
            User user = new User
            {
                UserName = postUserModel.UserName,
                FirstName = postUserModel.FirstName,
                LastName = postUserModel.LastName,
                Email = postUserModel.Email,
                Description = postUserModel.Description
            };

            IdentityResult result = await _userManager.CreateAsync(user, postUserModel.Password);

            if (!result.Succeeded)
            {
                string description = result.Errors.First().Description;

                if (description.Contains("is already taken"))
                {
                    description = "This username or email is already in use";
                }

                throw new IdentityException(description, this.GetType().Name, "PostUser", "400");
            }

            try
            {
                if (postUserModel.Roles == null)
                {
                    await _userManager.AddToRoleAsync(user, "Guest");
                }
                else
                {
                    await _userManager.AddToRolesAsync(user, postUserModel.Roles);
                }
            }
            catch (Exception e)
            {
                await _userManager.DeleteAsync(user);
                throw new IdentityException(e.Message, this.GetType().Name, "PostUser", "400");
            }

            return await GetUser(user.Id.ToString());
        }

        public async Task PutUser(string id, PutUserModel putUserModel)
        {
            if (_user.Claims.Where(x => x.Type.Contains("role")).Count() == 1 &&
                _user.IsInRole("Guest") &&
                _user.Identity.Name != id.ToString())
            {
                throw new ForbiddenException("No access to change this users data", this.GetType().Name, "PutUser", "403");
            }

            try
            {
                if (putUserModel.Roles == null)
                {
                    throw new IdentityException("User must have at least one role", this.GetType().Name, "PutUser", "400");
                }

                User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                if (user == null)
                {
                    throw new EntityException("User not found", this.GetType().Name, "PutUser", "404");
                }

                user.FirstName = putUserModel.FirstName;
                user.LastName = putUserModel.LastName;
                user.Email = putUserModel.Email;

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    string description = result.Errors.First().Description;

                    if (description.Contains("is already taken"))
                    {
                        description = "This email is already registered";
                    }

                    throw new IdentityException(description, this.GetType().Name, "PutUser", "400");
                }
                else
                {
                    await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                    await _userManager.AddToRolesAsync(user, putUserModel.Roles);
                }
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (e.GetType().Name.Equals("InvalidOperationException"))
                {
                    throw new IdentityException(e.Message, this.GetType().Name, "PutUser", "400");
                }

                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutUser", "400");
            }
        }

        public async Task PatchUser(string id, PatchUserModel patchUserModel)
        {
            if (_user.Claims.Where(x => x.Type.Contains("role")).Count() == 1 &&
                _user.IsInRole("Guest") &&
                _user.Identity.Name != id.ToString())
            {
                throw new ForbiddenException("No access to change this user's password", this.GetType().Name, "PatchUser", "403");
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (user == null)
            {
                throw new EntityException("User not found", this.GetType().Name, "PatchUser", "404");
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, patchUserModel.CurrentPassword, patchUserModel.NewPassword);

            if (!result.Succeeded)
            {
                throw new IdentityException(result.Errors.First().Description, this.GetType().Name, "PatchUser", "400");
            }
        }

        public async Task DeleteUser(string id)
        {   
            try
            {
                User user = await _context.Users
                    .Include(x => x.Favorites)
                    .Include(x => x.UserFollowers)
                    .Include(x => x.UserFollowing)
                    .Include( x=> x.Reviews)
                    .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                if (user == null)
                {
                    throw new EntityException("User not found", this.GetType().Name, "DeleteUser", "404");
                }

                _context.UserFollowers.RemoveRange(user.UserFollowers);
                _context.UserFollowers.RemoveRange(user.UserFollowing);
                _context.Favorites.RemoveRange(user.Favorites);
                _context.Reviews.RemoveRange(user.Reviews);

                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteUser", "400");
            }


        }

        public async Task<ResetPasswordModel> CreateResetToken(ForgotPasswordModel forgotPasswordModel)
        {
            User user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                ResetPasswordModel resetPasswordModel = new ResetPasswordModel
                {
                    Token = token,
                    Email = user.Email
                };

                return resetPasswordModel;
            }
            else
            {
                throw new EntityException("User not found", this.GetType().Name, "CreateResetToken", "500");
            }
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return;
            }
            else
            {
                throw new MovieMindException("Failed to reset password", this.GetType().Name, "ResetPassword", "500");
            }        
        }
        #endregion

        #region jwthandling
        public async Task<List<GetRefreshTokenModel>> GetUserRefreshTokens(Guid id)
        {
            List<GetRefreshTokenModel> refreshTokens = await _context.RefreshTokens
                .Where(x => x.UserId == id)
                .Select(x => new GetRefreshTokenModel
                {
                    Id = x.Id,
                    Token = x.Token,
                    Expires = x.Expires,
                    IsExpired = x.IsExpired,
                    Created = x.Created,
                    CreatedByIp = x.CreatedByIp,
                    Revoked = x.Revoked,
                    RevokedByIp = x.RevokedByIp,
                    ReplacedByToken = x.ReplacedByToken,
                    IsActive = x.IsActive
                })
                .AsNoTracking()
                .ToListAsync();

            if (refreshTokens.Count == 0)
            {
                throw new CollectionException("No refresh tokens found", this.GetType().Name, "GetUserRefreshTokens", "404");
            }

            return refreshTokens;
        }

        // JWT Methods
        // ===========

        public async Task<PostAuthenticateResponseModel> Authenticate(PostAuthenticateRequestModel postAuthenticateRequestModel, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.UserName == postAuthenticateRequestModel.UserName);

            if (user == null)
            {
                throw new UserNameException("Invalid username", this.GetType().Name, "Authenticate", "401");
            }

            // Verify password when user was found by UserName
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, postAuthenticateRequestModel.Password, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new PasswordException("Invalid password", this.GetType().Name, "Authenticate", "401");
            }

            // Authentication was successful so generate JWT and refresh tokens
            string jwtToken = await GenerateJwtToken(user);
            RefreshToken refreshToken = GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);

            await _userManager.UpdateAsync(user);

            return new PostAuthenticateResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                JwtToken = jwtToken,
                RefreshToken = refreshToken.Token,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task<PostAuthenticateResponseModel> RefreshToken(string token, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new TokenException("No user found with this token", this.GetType().Name, "RefreshToken", "401");
            }

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // Refresh token is no longer active
            if (!refreshToken.IsActive)
            {
                throw new RefreshTokenException("Refresh token is no longer valid", this.GetType().Name, "RefreshToken", "401");

            };

            // Replace old refresh token with a new one
            RefreshToken newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;

            // Generate new jwt
            string jwtToken = await GenerateJwtToken(user);

            user.RefreshTokens.Add(newRefreshToken);

            await _userManager.UpdateAsync(user);

            return new PostAuthenticateResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                JwtToken = jwtToken,
                RefreshToken = newRefreshToken.Token,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            User user = await _userManager.Users
                .Include(x => x.RefreshTokens)
                .FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                throw new TokenException("No user found with this token", this.GetType().Name, "RefreshToken", "401");
            }

            RefreshToken refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // Refresh token is no longer active
            if (!refreshToken.IsActive)
            {
                throw new RefreshTokenException("Refresh token is no longer valid", this.GetType().Name, "RefreshToken", "401");
            };

            // Revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;

            await _userManager.UpdateAsync(user);
        }


        // JWT helper methods
        // ==================

        private async Task<string> GenerateJwtToken(User user)
        {
            var roleNames = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));
            claims.Add(new Claim("UserName", user.UserName));

            foreach (string roleName in roleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "MovieMind web API",
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddMinutes(15), // cookie and refresh token lifetime must be the same
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        #endregion
    }
}
