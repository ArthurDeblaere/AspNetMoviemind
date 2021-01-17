using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Genres;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Get a list of all genres.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/genres
        ///
        /// </remarks>
        /// <returns>List of GetGenreModel</returns>
        /// <response code="200">Returns the list of genres</response>
        /// <response code="404">No genres were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetGenreModel>>> GetGenres()
        {
            return await _genreRepository.GetGenres(); 
        }

        /// <summary>
        /// Get a specific genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/genres/{id}
        ///
        /// </remarks>
        /// <returns>GetGenreModel</returns>
        /// <response code="200">Returns the genre</response>
        /// <response code="404">No genres were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetGenreModel>> GetGenre(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid genreId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetGenre", "400");
                }

                return await _genreRepository.GetGenre(id);
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
        /// Creates an genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/genres
        ///     {
        ///        "Name": "Action"
        ///     }
        ///
        /// </remarks>
        /// <param name="postGenreModel"></param>
        /// <returns>A newly created genre</returns>
        /// <response code="201">Returns the newly created genre</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<GetGenreModel>> PostGenre(PostGenreModel postGenreModel)
        {
            try
            {
                GetGenreModel genre = await _genreRepository.PostGenre(postGenreModel);

                return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Updates a genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/genres/{id}
        ///     {
        ///        "Name": "Action"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putGenreModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The genre could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> PutGenre(string id, PutGenreModel putGenreModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid genreId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutGenre", "400");
                }

                await _genreRepository.PutGenre(id, putGenreModel);

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
        /// Deletes a genre.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/genres/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The genre could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteGenre(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid genreId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteGenre", "400");
                }
                await _genreRepository.DeleteGenre(id);

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