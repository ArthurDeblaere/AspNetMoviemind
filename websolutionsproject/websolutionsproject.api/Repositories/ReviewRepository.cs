using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieMindContext _context;

        public ReviewRepository(MovieMindContext context)
        {
            _context = context;
        }

        public async Task<List<GetReviewModel>> GetReviews()
        {
            List<GetReviewModel> reviews = await _context.Reviews
              .Select(x => new GetReviewModel
              {
                  Id = x.Id,
                  Description = x.Description,
                  Rating = x.Rating,
                  Date = x.Date,
                  UserId = x.UserId,
                  User = new GetUserModel
                  {
                      Id = x.UserId,
                      FirstName = x.User.FirstName,
                      LastName = x.User.LastName,
                      Email = x.User.Email,
                      Description = x.User.Description,
                  },
                  MovieId = x.MovieId,
                  Movie = new GetMovieModel
                  {
                      Id = x.Movie.Id,
                      Description = x.Movie.Description,
                      DirectorId = x.Movie.DirectorId,
                      Duration = x.Movie.Duration,
                      GenreId = x.Movie.GenreId,
                      Name = x.Movie.Name,
                      Year = x.Movie.Year,
                      OverallRating = x.Movie.OverallRating
                  }
              })
              .AsNoTracking()
              .ToListAsync();

            return reviews;
        }

        public async Task<GetReviewModel> GetReview(string id)
        {
            GetReviewModel review = await _context.Reviews
               .Select(x => new GetReviewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Rating = x.Rating,
                   Date = x.Date,
                   UserId = x.UserId,
                   User = new GetUserModel
                   {
                       Id = x.UserId,
                       FirstName = x.User.FirstName,
                       LastName = x.User.LastName,
                       Email = x.User.Email,
                       Description = x.User.Description,
                   },
                   MovieId = x.MovieId,
                   Movie = new GetMovieModel
                   {
                       Id = x.Movie.Id,
                       Description = x.Movie.Description,
                       DirectorId = x.Movie.DirectorId,
                       Duration = x.Movie.Duration,
                       GenreId = x.Movie.GenreId,
                       Name = x.Movie.Name,
                       Year = x.Movie.Year,
                       OverallRating = x.Movie.OverallRating
                   }
               })
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            
            if (review == null)
            {
                throw new EntityException("Review not found", this.GetType().Name, "GetReview", "404");
            }

            return review;
        }

        public async Task<GetReviewModel> PostReview(PostReviewModel postReviewModel)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Id == postReviewModel.UserId);
            if (user == null)
            {
                throw new EntityException("User not found", this.GetType().Name, "PostReview", "404");
            }

            Movie movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == postReviewModel.MovieId);
            if (movie == null)
            {
                throw new EntityException("Movie not found", this.GetType().Name, "PostReview", "404");
            }

            try
            {
                EntityEntry<Review> result = await _context.Reviews.AddAsync(new Review
                {
                    Description = postReviewModel.Description,
                    Rating = postReviewModel.Rating,
                    Date = postReviewModel.Date,
                    UserId = postReviewModel.UserId,
                    User = user,
                    MovieId = postReviewModel.MovieId,
                    Movie = movie
                }); 

                await _context.SaveChangesAsync();

                return await GetReview(result.Entity.Id.ToString());
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PostReview", "400");
            }
        }

        public async Task PutReview(string id, PutReviewModel putReviewModel)
        {
            Review review = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (review == null)
            {
                throw new EntityException("Review not found", this.GetType().Name, "PutReview", "404");
            }

            try
            {
                review.Date = putReviewModel.Date;
                review.Description = putReviewModel.Description;
                review.Rating = putReviewModel.Rating;
                review.MovieId = putReviewModel.MovieId;
                review.UserId = putReviewModel.UserId;

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutReview", "400");
            }
           
        }

        public async Task DeleteReview(string id)
        {
            Review review = await _context.Reviews
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (review == null)
            {
                throw new EntityException("Review not found", this.GetType().Name, "DeleteReview", "404");
            }

            try
            {
                _context.Reviews.Remove(review);

                await _context.SaveChangesAsync();
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "DeleteReview", "400");
            }
        }
    }
}
