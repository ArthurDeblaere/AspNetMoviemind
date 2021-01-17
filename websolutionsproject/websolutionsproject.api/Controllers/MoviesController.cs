using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        /// <summary>
        /// Get a list of all movies.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/movies
        ///
        /// </remarks>
        /// <returns>List of GetMovieModel</returns>
        /// <response code="200">Returns the list of movies</response>
        /// <response code="404">No movies were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetMovieModel>>> GetMovies()
        {
            return await _movieRepository.GetMovies();
        }

        /// <summary>
        /// Get a specific actor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/movies/{id}
        ///
        /// </remarks>
        /// <returns>GetMovieModel</returns>
        /// <response code="200">Returns the movie</response>
        /// <response code="404">No movie were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetMovieModel>> GetMovie(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid movieId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetMovie", "400");
                }

                return await _movieRepository.GetMovie(id);
            }
            catch (Exception e)
            {
                if (e.Equals("404"))
                {
                    return NotFound(e.Message);
                }
                else
                {
                    return BadRequest(e.Message);
                }
            }
        }

        /// <summary>
        /// Creates a movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/movies
        ///     {
        ///        "Name": "Movie",
        ///        "Year": 2020,
        ///        "OverallRating": 8,
        ///        "Duration": 124,
        ///        "DirectorId": "5a0b0cc7-aa58-41e5-be77-d2a445d96769",
        ///        "GenreId": "68da0009-58c7-41d4-8ba9-775626cb96ff",
        ///        "Description": "A test movie starring no one."
        ///     }
        ///
        /// </remarks>
        /// <param name="postMovieModel"></param>
        /// <returns>A newly created movie</returns>
        /// <response code="201">Returns the newly created movie</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<GetMovieModel>> PostMovie(PostMovieModel postMovieModel)
        {
            try
            {
                GetMovieModel movie = await _movieRepository.PostMovie(postMovieModel);

                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Updates a movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/movies
        ///     {
        ///        "Name": "Movie 2",
        ///        "Year": 2020,
        ///        "OverallRating": 8,
        ///        "Duration": 124,
        ///        "DirectorId": "5a0b0cc7-aa58-41e5-be77-d2a445d96769",
        ///        "GenreId": "68da0009-58c7-41d4-8ba9-775626cb96ff",
        ///        "Description": "A test movie starring no one."
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putMovieModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The movie could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> PutMovie(string id, PutMovieModel putMovieModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid movieId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutMovie", "400");
                }

                await _movieRepository.PutMovie(id, putMovieModel);

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
        /// Deletes a movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/moves/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The movie could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid movieId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteMovie", "400");
                }

                await _movieRepository.DeleteMovie(id);

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
