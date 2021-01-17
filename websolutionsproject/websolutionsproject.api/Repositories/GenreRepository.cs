using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Genres;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MovieMindContext _context;

        public GenreRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetGenreModel>> GetGenres()
        {
            List<GetGenreModel> genres = await _context.Genres
                .Select(x => new GetGenreModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Movies = (from movie in _context.Movies
                             where movie.GenreId == x.Id
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

            return genres;
        }

        public async Task<GetGenreModel> GetGenre(string id)
        {
            GetGenreModel genre = await _context.Genres
                .Select(x => new GetGenreModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Movies = (from movie in _context.Movies
                              where movie.GenreId == x.Id
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

            if (genre == null)
            {
                throw new EntityException("Genre not found", this.GetType().Name, "GetGenre", "404");
            }
            return genre;
        }

        public async Task<GetGenreModel> PostGenre(PostGenreModel postGenreModel)
        {
            try
            {
                EntityEntry<Genre> result = await _context.Genres.AddAsync(new Genre
                {
                    Name = postGenreModel.Name
                });

                await _context.SaveChangesAsync();

                return await GetGenre(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostGenre", "400");
            }
        }

        public async Task PutGenre(string id, PutGenreModel putGenreModel)
        {
            Genre genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (genre == null)
            {
                throw new EntityException("Genre not found", this.GetType().Name, "PutGenre", "404");
            }

            try
            {
                genre.Name = putGenreModel.Name;

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutGenre", "400");
            }
            
        }

        public async Task DeleteGenre(string id)
        {
            Genre genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (genre == null)
            {
                throw new EntityException("Genre not found", this.GetType().Name, "PutGenre", "404");
            }

            try
            {
                _context.Genres.Remove(genre);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteGenre", "400");
            }
            
        }
    }
}
