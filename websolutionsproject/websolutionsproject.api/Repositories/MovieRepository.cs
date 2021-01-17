using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Genres;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieMindContext _context;

        public MovieRepository(MovieMindContext context)
        {
            _context = context;
        }
        public async Task<List<GetMovieModel>> GetMovies()
        {
            List<GetMovieModel> movies = await _context.Movies
               .Select(x => new GetMovieModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Year = x.Year,
                   OverallRating = x.OverallRating,
                   Duration = x.Duration,
                   Description = x.Description,
                   DirectorId = x.DirectorId,
                   Director = new GetDirectorModel
                   {
                       Id = x.Director.Id,
                       FirstName = x.Director.FirstName,
                       LastName = x.Director.LastName,
                       Birth = x.Director.Birth,
                       Nationality = x.Director.Nationality,
                       Description = x.Director.Description
                   },
                   GenreId = x.GenreId,
                   Genre = new GetGenreModel
                   {
                       Id = x.Genre.Id,
                       Name = x.Genre.Name
                   },
                   Actors = (from actorMovie in _context.ActorMovies
                             where actorMovie.MovieId == x.Id
                             select new GetActorModel
                             {
                                 Id = actorMovie.ActorId,
                                 FirstName = actorMovie.Actor.FirstName,
                                 LastName = actorMovie.Actor.LastName,
                                 Birth = actorMovie.Actor.Birth,
                                 Description = actorMovie.Actor.Description,
                                 Nationality = actorMovie.Actor.Nationality
                             }).ToList(),
                   Favorites = (from favorite in _context.Favorites
                                where favorite.MovieId == x.Id
                                select new GetUserModel
                                {
                                    Id = favorite.UserId,
                                    FirstName = favorite.User.FirstName,
                                    LastName = favorite.User.LastName,
                                    Email = favorite.User.Email,
                                    Description = favorite.User.Description,
                                }).ToList(),
                   Reviews = (from review in _context.Reviews
                              where review.MovieId == x.Id
                              select new GetReviewModel
                              {
                                  Id = review.Id,
                                  Rating = review.Rating,
                                  Date = review.Date,
                                  Description = review.Description,
                                  UserId = review.UserId,
                                  MovieId = review.MovieId
                              }).ToList()

               })
               .AsNoTracking()
               .ToListAsync();

            return movies;
        }

        public async Task<GetMovieModel> GetMovie(string id)
        {
            GetMovieModel movie = await _context.Movies
                .Select(x => new GetMovieModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    OverallRating = x.OverallRating,
                    Duration = x.Duration,
                    Description = x.Description,
                    DirectorId = x.DirectorId,
                    Director = new GetDirectorModel
                    {
                        Id = x.Director.Id,
                        FirstName = x.Director.FirstName,
                        LastName = x.Director.LastName,
                        Birth = x.Director.Birth,
                        Nationality = x.Director.Nationality,
                        Description = x.Director.Description
                    },
                    GenreId = x.GenreId,
                    Genre = new GetGenreModel
                    {
                        Id = x.Genre.Id,
                        Name = x.Genre.Name
                    },
                    Actors = (from actorMovie in _context.ActorMovies
                              where actorMovie.MovieId == x.Id
                              select new GetActorModel
                              {
                                  Id = actorMovie.ActorId,
                                  FirstName = actorMovie.Actor.FirstName,
                                  LastName = actorMovie.Actor.LastName,
                                  Birth = actorMovie.Actor.Birth,
                                  Description = actorMovie.Actor.Description,
                                  Nationality = actorMovie.Actor.Nationality
                              }).ToList(),
                    Favorites = (from favorite in _context.Favorites
                                 where favorite.MovieId == x.Id
                                 select new GetUserModel
                                 {
                                     Id = favorite.UserId,
                                     FirstName = favorite.User.FirstName,
                                     LastName = favorite.User.LastName,
                                     Email = favorite.User.Email,
                                     Description = favorite.User.Description,
                                 }).ToList(),
                    Reviews = (from review in _context.Reviews
                               where review.MovieId == x.Id
                               select new GetReviewModel
                               {
                                   Id = review.MovieId,
                                   Rating = review.Rating,
                                   Date = review.Date,
                                   Description = review.Description,
                                   UserId = review.UserId
                               }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "GetMovie", "404");
            }
            return movie;
        }

        public async Task<GetMovieModel> PostMovie(PostMovieModel postMovieModel)
        {
            Director director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == postMovieModel.DirectorId);
            if (director == null)
            {
                throw new EntityException("Director not found", this.GetType().Name, "PostMovie", "404");
            }

            Genre genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == postMovieModel.GenreId);
            if (genre == null)
            {
                throw new EntityException("Genre not found", this.GetType().Name, "PostMovie", "404");
            }

            try
            {
                EntityEntry<Movie> result = await _context.Movies.AddAsync(new Movie
                {
                    Name = postMovieModel.Name,
                    Year = postMovieModel.Year,
                    OverallRating = postMovieModel.OverallRating,
                    Duration = postMovieModel.Duration,
                    Description = postMovieModel.Description,
                    DirectorId = postMovieModel.DirectorId,
                    Director = director,
                    GenreId = postMovieModel.GenreId,
                    Genre = genre
                });

                await _context.SaveChangesAsync();

                return await GetMovie(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostMovie", "400");
            }
        }

        public async Task PutMovie(string id, PutMovieModel putMovieModel)
        {
            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "PutMovie", "404");
            }

            Director director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == putMovieModel.DirectorId);
            if (director == null)
            {
                throw new EntityException("Director not found", this.GetType().Name, "PutMovie", "404");
            }

            Genre genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == putMovieModel.GenreId);
            if (genre == null)
            {
                throw new EntityException("Genre not found", this.GetType().Name, "PutMovie", "404");
            }

            try
            {
                movie.Name = putMovieModel.Name;
                movie.Year = putMovieModel.Year;
                movie.OverallRating = putMovieModel.OverallRating;
                movie.Duration = putMovieModel.Duration;
                movie.Description = putMovieModel.Description;
                movie.GenreId = putMovieModel.GenreId;
                movie.Genre = genre;
                movie.DirectorId = putMovieModel.DirectorId;
                movie.Director = director;

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutMovie", "400");
            }

        }

        public async Task DeleteMovie(string id)
        {
            Movie movie = await _context.Movies
               .Include(x => x.ActorMovies)
               .Include(x => x.Favorites)
               .Include(x => x.Reviews)
               .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "DeleteMovie", "404");
            }

            try
            {
                _context.ActorMovies.RemoveRange(movie.ActorMovies);
                _context.Favorites.RemoveRange(movie.Favorites);
                _context.Reviews.RemoveRange(movie.Reviews);

                _context.Movies.Remove(movie);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteMovie", "400");
            }
           

        }

        public async Task CalculateOverallRating(string id)
        {
            Movie movie = await _context.Movies
               .Include(x => x.ActorMovies)
               .Include(x => x.Favorites)
               .Include(x => x.Reviews)
               .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "CalculateOverallRating", "404");
            }

            try
            {
                List<int> reviewRatings = (from review in _context.Reviews
                                                       where review.MovieId == movie.Id
                                                       select review.Rating).ToList();


                double average = reviewRatings.Count > 0 ? reviewRatings.Average() : 0.0;
                movie.OverallRating = Convert.ToInt32(average);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "CalculateOverallRating", "400");
            }
        }
    }
}
