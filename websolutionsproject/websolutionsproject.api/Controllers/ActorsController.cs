using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Actors;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;

        public ActorsController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        /// <summary>
        /// Get a list of all actors.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/actors
        ///
        /// </remarks>
        /// <returns>List of GetActorModel</returns>
        /// <response code="200">Returns the list of actors</response>
        /// <response code="404">No actors were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetActorModel>>> GetActors()
        {
           return await _actorRepository.GetActors();
        }

        /// <summary>
        /// Get a specific actor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/actors/{id}
        ///
        /// </remarks>
        /// <returns>GetActorModel</returns>
        /// <response code="200">Returns the actor</response>
        /// <response code="404">No actors were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetActorModel>> GetActor(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid actorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetActor", "400");
                }

                return await _actorRepository.GetActor(id);
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
        /// Creates an actor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/actors
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Doe",
        ///        "Nationality": "American",
        ///        "Birth": "01/07/1970",
        ///        "Description": "New user"
        ///     }
        ///
        /// </remarks>
        /// <param name="postActorModel"></param>
        /// <returns>A newly created actor</returns>
        /// <response code="201">Returns the newly created actor</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<ActionResult<GetActorModel>> PostActor(PostActorModel postActorModel)
        {
            try
            {
                GetActorModel actor = await _actorRepository.PostActor(postActorModel);

                return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, actor);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }

        }

        /// <summary>
        /// Updates an actor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/actors/{id}
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Doe",
        ///        "Nationality": "American",
        ///        "Birth": "01/07/1970",
        ///        "Description": "New user"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putActorModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The actor could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> PutActor(string id, PutActorModel putActorModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid actorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutActor", "400");
                }

                await _actorRepository.PutActor(id, putActorModel);

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
        /// Deletes an actor.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/actors/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The actor could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteActor(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid actorId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteActor", "400");
                }

                await _actorRepository.DeleteActor(id);

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
