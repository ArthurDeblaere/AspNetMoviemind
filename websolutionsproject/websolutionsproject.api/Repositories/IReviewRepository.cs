using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Reviews;

namespace websolutionsproject.api.Repositories
{
    public interface IReviewRepository
    {
        Task<List<GetReviewModel>> GetReviews();
        Task<GetReviewModel> GetReview(string id);
        //Task<GetReviewModel> GetUserReviews(string userId);
        //Task<GetReviewModel> GetMovieReviews(string movieId);
        Task<GetReviewModel> PostReview(PostReviewModel postReviewModel);
        Task PutReview(string id, PutReviewModel putReviewModel);
        Task DeleteReview(string id);
    }
}
