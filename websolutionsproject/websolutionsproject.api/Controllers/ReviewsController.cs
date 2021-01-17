using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Reviews;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;

        public ReviewsController(IReviewRepository reviewRepository, IMovieRepository movieRepository)
        {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get a list of all reviews.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/reviews
        ///
        /// </remarks>
        /// <returns>List of GetReviewModel</returns>
        /// <response code="200">Returns the list of reviews</response>
        /// <response code="404">No reviews were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetReviewModel>>> GetReviews()
        {
            return await _reviewRepository.GetReviews();
        }

        /// <summary>
        /// Get a specific review.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/reviews/{id}
        ///
        /// </remarks>
        /// <returns>GetReviewModel</returns>
        /// <response code="200">Returns the review</response>
        /// <response code="404">No reviews were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetReviewModel>> GetReview(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid reviewId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetReview", "400");
                }

                return await _reviewRepository.GetReview(id);
            }
            catch (MovieMindException e)
            {
                if (e.MovieMindError.Status.Equals("404"))
                {
                    return NotFound(e.MovieMindError);
                }
                else
                {
                    return BadRequest(e.MovieMindError);
                }
            }
        }

        /// <summary>
        /// Creates a review.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/reviews
        ///     {
        ///        "Description": "A fantastic movie",
        ///        "Rating": 9,
        ///        "Date": "07/11/2020",
        ///        "UserId": "4a23fda4-c63e-4c26-be6b-3ab5e0c1381e",
        ///        "MovieId": "67cf9787-789d-4463-82e8-be4e0dc71e98"
        ///     }
        ///
        /// </remarks>
        /// <param name="postReviewModel"></param>
        /// <returns>A newly created review</returns>
        /// <response code="201">Returns the newly created review</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetReviewModel>> PostReview(PostReviewModel postReviewModel)
        {
            try
            {
                GetReviewModel review = await _reviewRepository.PostReview(postReviewModel);

                await _movieRepository.CalculateOverallRating(postReviewModel.MovieId.ToString());

                return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Updates a review.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/reviews
        ///     {
        ///        "Description": "A moderate movie",
        ///        "Rating": 6,
        ///        "Date": "07/11/2020",
        ///        "UserId": "4a23fda4-c63e-4c26-be6b-3ab5e0c1381e",
        ///        "MovieId": "67cf9787-789d-4463-82e8-be4e0dc71e98"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putReviewModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The review could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> PutReview(string id, PutReviewModel putReviewModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid reviewId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutReview", "400");
                }

                await _reviewRepository.PutReview(id, putReviewModel);

                await _movieRepository.CalculateOverallRating(putReviewModel.MovieId.ToString());

                return NoContent();
            }
            catch (MovieMindException e)
            {
                if (e.MovieMindError.Status.Equals("404"))
                {
                    return NotFound(e.MovieMindError);
                }
                else
                {
                    return BadRequest(e.MovieMindError);
                }
            }
        }

        /// <summary>
        /// Deletes a review.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/reviews/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The review could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid reviewId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteReview", "400");
                }

                await _reviewRepository.DeleteReview(id);

                return NoContent();
            }
            catch (MovieMindException e)
            {
                if (e.MovieMindError.Status.Equals("404"))
                {
                    return NotFound(e.MovieMindError);
                }
                else
                {
                    return BadRequest(e.MovieMindError);
                }
            }
        }
    }
}
