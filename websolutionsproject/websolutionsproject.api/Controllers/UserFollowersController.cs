using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.UserFollowers;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserFollowersController : ControllerBase
    {
        private readonly IUserFollowerRepository _userFollowerRepository;

        public UserFollowersController(IUserFollowerRepository userFollowerRepository)
        {
            _userFollowerRepository = userFollowerRepository;
        }

        /// <summary>
        /// Get all userfollower models.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userfollowers
        ///
        /// </remarks>
        /// <returns>List of GetUserFollowerModel</returns>
        /// <response code="200">Returns the user follower model</response>
        /// <response code="404">No users were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        /// 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetUserFollowerModel>>> GetUserFollowers()
        {
            return await _userFollowerRepository.GetUserFollowers();
        }

        /// <summary>
        /// Get specific userfollower model.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/userfollowers/{id}
        ///
        /// </remarks>
        /// <returns>GetUserFollowerModel</returns>
        /// <response code="200">Returns the user follower model</response>
        /// <response code="404">No users were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        /// 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetUserFollowerModel>> GetUserFollower(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid userFollowerId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetUserFollower", "400");
                }
                return await _userFollowerRepository.GetUserFollower(id);
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
        /// Creates a user follower model.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/userfollowers
        ///     {
        ///        "FollowingId": "f0a23d94-0636-43bc-a084-26f93f81b727",
        ///        "FollowerId": "931d6370-09cc-4dd8-8eb2-60ca06811484"
        ///     }
        ///
        /// </remarks>
        /// <param name="postUserFollowerModel"></param>
        /// <returns>A newly created userfollower</returns>
        /// <response code="201">Returns the newly created userfollower</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetUserFollowerModel>> PostUserFollower(PostUserFollowerModel postUserFollowerModel)
        {
            try
            {
                GetUserFollowerModel userFollowerModel = await _userFollowerRepository.PostUserFollower(postUserFollowerModel);

                return CreatedAtAction(nameof(GetUserFollower), new { id = userFollowerModel.Id }, userFollowerModel);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }

        }

        /// <summary>
        /// Deletes an user follower model.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/userfollowers/{id}
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
                if (!Guid.TryParse(id, out Guid userFollowerId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteUserFollower", "400");
                }
                await _userFollowerRepository.DeleteUserFollower(id);

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
