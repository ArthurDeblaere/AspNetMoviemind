using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Directors;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class DirectorsController : ControllerBase
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorsController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        /// <summary>
        /// Get a list of all directors.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/directors
        ///
        /// </remarks>
        /// <returns>List of GetDirectorModel</returns>
        /// <response code="200">Returns the list of directors</response>
        /// <response code="404">No directors were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetDirectorModel>>> GetDirectors()
        {
            return await _directorRepository.GetDirectors();
        }

        /// <summary>
        /// Get a specific director.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/directors/{id}
        ///
        /// </remarks>
        /// <returns>GetDirectorModel</returns>
        /// <response code="200">Returns the directors</response>
        /// <response code="404">No directors were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetDirectorModel>> GetDirector(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid directorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetDirector", "400");
                }
                return await _directorRepository.GetDirector(id);
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
        /// Creates an director.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/directors
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Doe",
        ///        "Nationality": "American",
        ///        "Birth": "01/07/1970",
        ///        "Description": "New director"
        ///     }
        ///
        /// </remarks>
        /// <param name="postDirectorModel"></param>
        /// <returns>A newly created director</returns>
        /// <response code="201">Returns the newly created director</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<GetDirectorModel>> PostDirector(PostDirectorModel postDirectorModel)
        {
            try
            {
                GetDirectorModel director = await _directorRepository.PostDirector(postDirectorModel);

                return CreatedAtAction(nameof(GetDirector), new { id = director.Id }, director);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Updates a director.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/directors/{id}
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Doe",
        ///        "Nationality": "American",
        ///        "Birth": "01/07/1970",
        ///        "Description": "New description for this director"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putDirectorModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The director could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> PutDirector(string id, PutDirectorModel putDirectorModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid directorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutDirector", "400");
                }

                await _directorRepository.PutDirector(id, putDirectorModel);

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
        /// Deletes a director.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/directors/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The director could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteDirector(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid directorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteDirector", "400");
                }
                await _directorRepository.DeleteDirector(id);

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
