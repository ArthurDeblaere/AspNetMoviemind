using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Favorites;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly MovieMindContext _context;

        public FavoriteRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<GetFavoriteModel> GetFavorite(string id)
        {
            GetFavoriteModel favorite = await _context.Favorites
                .Where(x => x.Id == Guid.Parse(id))
                .Select(x => new GetFavoriteModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = new GetUserModel
                    {
                        Id = x.UserId,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Email = x.User.Email,
                        Description = x.User.Description
                    },
                    MovieId = x.MovieId,
                    Movie = new GetMovieModel
                    {
                        Id = x.MovieId,
                        DirectorId = x.Movie.DirectorId,
                        Duration = x.Movie.Duration,
                        GenreId = x.Movie.GenreId,
                        Description = x.Movie.Description,
                        Name = x.Movie.Name,
                        OverallRating = x.Movie.OverallRating
                    }
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (favorite == null)
            {
                throw new EntityException("Favorite not found", this.GetType().Name, "GetFavorite", "404");
            }
            
            return favorite;

        }
        public async Task<List<GetFavoriteModel>> GetFavorites()
        {
            List<GetFavoriteModel> favorites = await _context.Favorites
                .Select(x => new GetFavoriteModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    User = new GetUserModel
                    {
                        Id = x.UserId,
                        FirstName = x.User.FirstName,
                        LastName =x.User.LastName,
                        Email = x.User.Email,
                        Description = x.User.Description
                    },
                    MovieId = x.MovieId,
                    Movie = new GetMovieModel
                    {
                        Id = x.MovieId,
                        DirectorId = x.Movie.DirectorId,
                        Duration = x.Movie.Duration,
                        GenreId = x.Movie.GenreId,
                        Description = x.Movie.Description,
                        Name = x.Movie.Name,
                        OverallRating = x.Movie.OverallRating
                    }
                })
                .AsNoTracking()
                .ToListAsync();

            return favorites;
        }

        public async Task<GetFavoriteModel> PostFavorite(PostFavoriteModel postFavoriteModel)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == postFavoriteModel.UserId);
            if (user == null)
            {
                throw new EntityException("User not found", this.GetType().Name, "PostFavorite", "404");
            }

            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == postFavoriteModel.MovieId);
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "PostFavorite", "404");
            }

            try
            {
                EntityEntry<Favorite> result = await _context.Favorites.AddAsync(new Favorite
                {
                    UserId = postFavoriteModel.UserId,
                    User = user,
                    MovieId = postFavoriteModel.MovieId,
                    Movie = movie
                });

                await _context.SaveChangesAsync();

                return await GetFavorite(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostFavorite", "400");
            }
            
        }

        public async Task PutFavorite(string id, PutFavoriteModel putFavoriteModel)
        {
            Favorite favorites = await _context.Favorites.FirstOrDefaultAsync(x => x.UserId == putFavoriteModel.UserId && x.Id == Guid.Parse(id));
            if (favorites == null)
            {
                throw new EntityException("Favorite not found", this.GetType().Name, "PutFavorite", "404");
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == putFavoriteModel.UserId);
            if (user == null)
            {
                throw new EntityException("User not found", this.GetType().Name, "PutFavorite", "404");
            }

            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == putFavoriteModel.MovieId);
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "PutFavorite", "404");
            }

            try
            {
                favorites.Movie = movie;

                favorites.MovieId = putFavoriteModel.MovieId;
                favorites.User = user;
                favorites.UserId = putFavoriteModel.UserId;

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutFavorite", "400");
            }
        }
        public async Task DeleteFavorite(string id)
        {
            try
            {
                Favorite favorite = await _context.Favorites
                    .Where(x => x.Id == Guid.Parse(id))
                    .FirstOrDefaultAsync();

                _context.Favorites.Remove(favorite);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteFavorite", "400");
            }

        }
    }
}
