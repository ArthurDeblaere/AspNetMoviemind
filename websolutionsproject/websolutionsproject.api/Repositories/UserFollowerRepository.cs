using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.UserFollowers;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class UserFollowerRepository : IUserFollowerRepository
    {
        private readonly MovieMindContext _context;

        public UserFollowerRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetUserFollowerModel>> GetUserFollowers()
        {
            List<GetUserFollowerModel> userFollowers = await _context.UserFollowers
                .Select(x => new GetUserFollowerModel
                {
                    Id = x.Id,
                    FollowingId = x.FollowingId,
                    Following = new GetUserModel
                    {
                        Id = x.FollowingId,
                        FirstName = x.Following.FirstName,
                        LastName = x.Following.LastName,
                        Email = x.Following.Email,
                        Description = x.Following.Description
                    },
                    FollowerId = x.FollowerId,
                    Follower = new GetUserModel
                    {
                        Id = x.FollowerId,
                        FirstName = x.Follower.FirstName,
                        LastName = x.Follower.LastName,
                        Email = x.Follower.Email,
                        Description = x.Follower.Description
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            return userFollowers;

        }

        public async Task<GetUserFollowerModel> GetUserFollower(string id)
        {
            GetUserFollowerModel userFollower = await _context.UserFollowers
                .Where(x => x.Id == Guid.Parse(id))
                .Select(x => new GetUserFollowerModel
                {
                    Id = x.Id,
                    FollowingId = x.FollowingId,
                    Following = new GetUserModel
                    {
                        Id = x.FollowingId,
                        FirstName = x.Following.FirstName,
                        LastName = x.Following.LastName,
                        Email = x.Following.Email,
                        Description = x.Following.Description
                    },
                    FollowerId = x.FollowerId,
                    Follower = new GetUserModel
                    {
                        Id = x.FollowerId,
                        FirstName = x.Follower.FirstName,
                        LastName = x.Follower.LastName,
                        Email = x.Follower.Email,
                        Description = x.Follower.Description
                    }
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (userFollower == null)
            {
                throw new EntityException("UserFollower not found", this.GetType().Name, "GetUserFollower", "404");
            }

            return userFollower;

        }

        public async Task<GetUserFollowerModel> PostUserFollower(PostUserFollowerModel postUserFollowerModel)
        {
            User following = await _context.Users.FirstOrDefaultAsync(x => x.Id == postUserFollowerModel.FollowingId);
            if (following == null)
            {
                throw new EntityException("User following not found", this.GetType().Name, "PostUserFollower", "404");
            }

            User follower = await _context.Users.FirstOrDefaultAsync(x => x.Id == postUserFollowerModel.FollowerId);
            if (follower == null)
            {
                throw new EntityException("User follower not found", this.GetType().Name, "PostUserFollower", "404");
            }

            try
            {
                EntityEntry<UserFollower> result = await _context.UserFollowers.AddAsync(new UserFollower
                {
                    FollowingId = postUserFollowerModel.FollowingId,
                    Following = following,
                    FollowerId = postUserFollowerModel.FollowerId,
                    Follower = follower
                });

                await _context.SaveChangesAsync();

                return await GetUserFollower(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostUserFollower", "400");
            }

        }

        public async Task DeleteUserFollower(string id)
        {
            UserFollower userFollower = await _context.UserFollowers
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (userFollower == null)
            {
                throw new EntityException("User follower not found", this.GetType().Name, "DeleteUserFollower", "404");
            }

            try
            {
                _context.UserFollowers.Remove(userFollower);

                await _context.SaveChangesAsync();

            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteUserFollower", "400");
            }
        }
    }
}
