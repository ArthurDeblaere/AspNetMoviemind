using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.ActorMovies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    //[Route("api/actormovies/test1")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ActorMoviesController : ControllerBase
    {
        public readonly IActorMovieRepository _actorMovieRepository;
        public ActorMoviesController(IActorMovieRepository actorMovieRepository)
        {
            _actorMovieRepository = actorMovieRepository;
        }
        /// <summary>
        /// Get a specific actormovie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/actormovies/{id}
        ///
        /// </remarks>
        /// <returns>GetActorMovieModel</returns>
        /// <response code="200">Returns the actormovie</response>
        /// <response code="404">No actormovies were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetActorMovieModel>> GetActorMovie(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid actorMovieId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetActorMovie", "400");
                }

                return await _actorMovieRepository.GetActorMovie(id);
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
        /// Get a list of actormoviemodels
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/actormovies
        ///
        /// </remarks>
        /// <returns>List of GetActorMovieModel</returns>
        /// <response code="200">Returns the list of actor movies</response>
        /// <response code="404">No actors were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetActorMovieModel>>> GetActorMovies()
        {
            return await _actorMovieRepository.GetActorMovies();
        }

        /// <summary>
        /// Creates an actormovie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/actormovies
        ///     {
        ///        "ActorId": "09185c8a-852f-4ca4-b86c-35a6f7d02cbc",
        ///        "MovieId": "87248dea-44c2-4d03-8f01-f633a936ff5d"
        ///     }
        ///
        /// </remarks>
        /// <param name="postActorMovieModel"></param>
        /// <returns>A newly created actormovie</returns>
        /// <response code="201">Returns the newly created actormovie</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<GetActorMovieModel>> PostActorMovie(PostActorMovieModel postActorMovieModel)
        {
            try
            {
                GetActorMovieModel getActorMovieModel = await _actorMovieRepository.PostActorMovie(postActorMovieModel);

                return CreatedAtAction(nameof(GetActorMovie), new { id = getActorMovieModel.Id }, getActorMovieModel);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Deletes an actor movie model.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/actormovies/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user follower could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteUserFollower(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid actorMovieId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteActorMovie", "400");
                }

                await _actorMovieRepository.DeleteActorMovie(id);

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

