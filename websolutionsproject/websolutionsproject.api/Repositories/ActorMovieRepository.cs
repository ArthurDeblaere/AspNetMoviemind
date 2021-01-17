using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.ActorMovies;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class ActorMovieRepository : IActorMovieRepository
    {
        private readonly MovieMindContext _context;

        public ActorMovieRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetActorMovieModel>> GetActorMovies()
        {
            List<GetActorMovieModel> actorMovies = await _context.ActorMovies
              .Select(x => new GetActorMovieModel
              {
                  Id = x.Id,
                  ActorId = x.ActorId,
                  Actor = new GetActorModel
                  {
                      Id = x.Actor.Id,
                      FirstName = x.Actor.FirstName,
                      LastName = x.Actor.LastName,
                      Birth = x.Actor.Birth,
                      Nationality = x.Actor.Nationality,
                      Description = x.Actor.Description
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

            return actorMovies;
        }

        public async Task<GetActorMovieModel> GetActorMovie(string id)
        {
            GetActorMovieModel actorMovie = await _context.ActorMovies
                .Where(x => x.Id == Guid.Parse(id))
                .Select(x => new GetActorMovieModel
                {
                    Id = x.Id,
                    ActorId = x.ActorId,
                    Actor = new GetActorModel
                    {
                        Id = x.Actor.Id,
                        FirstName = x.Actor.FirstName,
                        LastName = x.Actor.LastName,
                        Birth = x.Actor.Birth,
                        Nationality = x.Actor.Nationality,
                        Description = x.Actor.Description
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

            if (actorMovie == null)
            {
                throw new EntityException("Actormovie not found", this.GetType().Name, "GetActorMovie", "404");
            }
            
            return actorMovie;

        }

        public async Task<GetActorMovieModel> PostActorMovie(PostActorMovieModel postActorMovieModel)
        {
            Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == postActorMovieModel.ActorId);
            if (actor == null)
            {
                throw new EntityException("Actor not found", this.GetType().Name, "PostActorMovie", "404");
            }
            
            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == postActorMovieModel.MovieId);
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "PostActorMovie", "404");
            }

            try
            {
                EntityEntry<ActorMovie> result = await _context.ActorMovies.AddAsync(new ActorMovie
                {
                    ActorId = postActorMovieModel.ActorId,
                    Actor = actor,
                    MovieId = postActorMovieModel.MovieId,
                    Movie = movie
                });

                await _context.SaveChangesAsync();

                return await GetActorMovie(result.Entity.Id.ToString());
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostActorMovie", "400");
            }


        }

        public async Task PutActorMovie(string id, PutActorMovieModel putActorMovieModel)
        {
            try
            {
                ActorMovie actorMovie = await _context.ActorMovies.FirstOrDefaultAsync((x => x.Id == Guid.Parse(id)));
                if (actorMovie == null)
                {
                    throw new EntityException("Actormovie not found", this.GetType().Name, "PutActorMovie", "404");
                }

                Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == putActorMovieModel.ActorId);
                if (actor == null)
                {
                    throw new EntityException("Actor not found", this.GetType().Name, "PutActorMovie", "404");
                }

                Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == putActorMovieModel.MovieId);
                if (movie == null)
                {
                    throw new EntityException("Movie not found", this.GetType().Name, "PutActorMovie", "404");
                }

                actorMovie.ActorId = putActorMovieModel.ActorId;
                actorMovie.Actor = actor;
                actorMovie.MovieId = putActorMovieModel.MovieId;
                actorMovie.Movie = movie;

                await _context.SaveChangesAsync();
            }
            catch(MovieMindException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutActorMovie", "400");
            }
            
        }

        public async Task DeleteActorMovie(string id)
        {
            try
            {
                ActorMovie actorMovie = await _context.ActorMovies.FirstOrDefaultAsync((x => x.Id == Guid.Parse(id)));

                if (actorMovie == null)
                {
                    throw new EntityException("Actormovie not found", this.GetType().Name, "DeleteActorMovie", "404");
                }

                _context.ActorMovies.Remove(actorMovie);
                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutActorMovie", "400");
            }
        }
    }
}
