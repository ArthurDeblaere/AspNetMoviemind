using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieMindContext _context;

        public ActorRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetActorModel>> GetActors()
        {
            List<GetActorModel> actors = await _context.Actors
                .Select(x => new GetActorModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birth = x.Birth,
                    Nationality = x.Nationality,
                    Description = x.Description,
                    Movies = (from actorMovie in _context.ActorMovies
                             where actorMovie.ActorId == x.Id
                             select new GetMovieModel
                             {
                                 Id = actorMovie.Id,
                                 Name = actorMovie.Movie.Name,
                                 Description = actorMovie.Movie.Description,
                                 Duration = actorMovie.Movie.Duration,
                                 Year = actorMovie.Movie.Year,
                                 OverallRating = actorMovie.Movie.OverallRating,
                                 GenreId = actorMovie.Movie.GenreId,
                                 DirectorId = actorMovie.Movie.DirectorId
                             }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            return actors;
        }

        public async Task<GetActorModel> GetActor(string id)
        {
            GetActorModel actor = await _context.Actors
                .Select(x => new GetActorModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birth = x.Birth,
                    Nationality = x.Nationality,
                    Description = x.Description,
                    Movies = (from actorMovie in _context.ActorMovies
                              where actorMovie.ActorId == x.Id
                              select new GetMovieModel
                              {
                                  Id = actorMovie.MovieId,
                                  Name = actorMovie.Movie.Name,
                                  Description = actorMovie.Movie.Description,
                                  Duration = actorMovie.Movie.Duration,
                                  Year = actorMovie.Movie.Year,
                                  OverallRating = actorMovie.Movie.OverallRating,
                                  GenreId = actorMovie.Movie.GenreId,
                                  DirectorId = actorMovie.Movie.DirectorId
                              }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (actor == null)
            {
                throw new EntityException("Actor not found", this.GetType().Name, "GetActor", "404");
            }
            return actor;
        }


        public async Task<GetActorModel> PostActor(PostActorModel postActorModel)
        {
            try
            {
                EntityEntry<Actor> result = await _context.Actors.AddAsync(new Actor
                {
                    FirstName = postActorModel.FirstName,
                    LastName = postActorModel.LastName,
                    Birth = postActorModel.Birth,
                    Nationality = postActorModel.Nationality,
                    Description = postActorModel.Description
                });

                await _context.SaveChangesAsync(); 
                
                return await GetActor(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostActor", "400");
            }
        }

        public async Task PutActor(string id, PutActorModel putActorModel)
        {
            try
            {
                Actor actor = await _context.Actors.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                actor.FirstName = putActorModel.FirstName;
                actor.LastName = putActorModel.LastName;
                actor.Birth = putActorModel.Birth;
                actor.Nationality = putActorModel.Nationality;
                actor.Description = putActorModel.Description;

                await _context.SaveChangesAsync();
            }
            catch(MovieMindException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutActor", "400");
            }
        }

        public async Task DeleteActor(string id)
        {
            try
            {
                Actor actor = await _context.Actors
                .Include(x => x.ActorMovies)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                if (actor == null)
                {
                    throw new EntityException("Actor not found", this.GetType().Name, "DeleteActor", "404");
                }

                _context.ActorMovies.RemoveRange(actor.ActorMovies);
                _context.Actors.Remove(actor);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutActor", "400");
            }
            
        }
    }
}
