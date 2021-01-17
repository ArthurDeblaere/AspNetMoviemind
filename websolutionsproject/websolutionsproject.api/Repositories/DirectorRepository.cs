using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieMindContext _context;

        public DirectorRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetDirectorModel>> GetDirectors()
        {
            List<GetDirectorModel> directors = await _context.Directors
                .Select(x => new GetDirectorModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birth = x.Birth,
                    Nationality = x.Nationality,
                    Description = x.Description,
                    Movies = (from movie in _context.Movies
                              where movie.DirectorId == x.Id
                              select new GetMovieModel
                              {
                                  Id = movie.Id,
                                  Name = movie.Name,
                                  Description = movie.Description,
                                  Duration = movie.Duration,
                                  Year = movie.Year,
                                  OverallRating = movie.OverallRating,
                                  GenreId = movie.GenreId,
                                  DirectorId = movie.DirectorId
                              }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            return directors;
        }

        public async Task<GetDirectorModel> GetDirector(string id)
        {
            GetDirectorModel director = await _context.Directors
                .Select(x=> new GetDirectorModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birth = x.Birth,
                    Nationality = x.Nationality,
                    Description = x.Description,
                    Movies = (from movie in _context.Movies
                              where movie.DirectorId == x.Id
                              select new GetMovieModel
                              {
                                  Id = movie.Id,
                                  Name = movie.Name,
                                  Description = movie.Description,
                                  Duration = movie.Duration,
                                  Year = movie.Year,
                                  OverallRating = movie.OverallRating,
                                  GenreId = movie.GenreId,
                                  DirectorId = movie.DirectorId
                              }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (director == null)
            {
                throw new EntityException("Director not found", this.GetType().Name, "GetDirector", "404");
            }

            return director;
        }

        public async Task<GetDirectorModel> PostDirector(PostDirectorModel postDirectorModel)
        {
            try
            {
                DateTime birth = Convert.ToDateTime(postDirectorModel.Birth);

                EntityEntry<Director> result = await _context.Directors.AddAsync(new Director
                {
                    FirstName = postDirectorModel.FirstName,
                    LastName = postDirectorModel.LastName,
                    Birth = birth,
                    Nationality = postDirectorModel.Nationality,
                    Description = postDirectorModel.Description
                });
                await _context.SaveChangesAsync();

                return await GetDirector(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostDirector", "400");
            }
        }

        public async Task PutDirector(string id, PutDirectorModel putDirectorModel)
        {           
            try
            {
                DateTime birth = Convert.ToDateTime(putDirectorModel.Birth);

                Director director = await _context.Directors.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                director.FirstName = putDirectorModel.FirstName;
                director.LastName = putDirectorModel.LastName;
                director.Birth = birth;
                director.Nationality = putDirectorModel.Nationality;
                director.Description = putDirectorModel.Description;
                
                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutDirector", "400");
            }
        }

        public async Task DeleteDirector(string id)
        {
            try
            {
                Director director = await _context.Directors
                .Include(x => x.Movies)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                //_context.Movies.RemoveRange(director.Movies);
                _context.Directors.Remove(director);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteDirector", "400");
            }
            
        }
    }
}
