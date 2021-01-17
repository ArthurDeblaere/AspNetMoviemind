using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Favorites;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoritesController(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        /// <summary>
        /// Get all favorites.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/favorites
        ///
        /// </remarks>
        /// <returns>List of GetFavoriteModel</returns>
        /// <response code="200">Returns the favorites</response>
        /// <response code="404">No favorites or user were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<List<GetFavoriteModel>>> GetFavorites()
        {
            return await _favoriteRepository.GetFavorites();
            
        }


        /// <summary>
        /// Get specific favorite.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/favorites/{id}
        ///
        /// </remarks>
        /// <returns>GetFavoriteModel</returns> 
        /// <response code="200">Returns the favorite movie</response>
        /// <response code="404">No favorite or user were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetFavoriteModel>> GetFavorite(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid favoriteId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetFavorite", "400");
                }

                return await _favoriteRepository.GetFavorite(id);
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
        /// Creates an favorite relationship.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/users/favorites
        ///     {
        ///        "UserId": "7138ec7e-d694-4333-8118-8791abe50fb3",
        ///        "MovieId": "1726dcb8-7e3d-47e5-ba71-134fe7c5de5d"
        ///     }
        ///
        /// </remarks>
        /// <param name="postFavoriteModel"></param>
        /// <returns>A newly created favorite</returns>
        /// <response code="201">Returns the newly created favorite</response>
        /// <response code="400">If something went wrong while saving into the database</response>    
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Guest")]
        public async Task<ActionResult<GetFavoriteModel>> PostFavorite(PostFavoriteModel postFavoriteModel)
        {
            try
            {
                GetFavoriteModel favorite = await _favoriteRepository.PostFavorite(postFavoriteModel);

                return CreatedAtAction(nameof(GetFavorite), new { id = favorite.Id }, favorite);
            }
            catch (DatabaseException e)
            {
                return BadRequest(e.MovieMindError);
            }
        }

        /// <summary>
        /// Updates a favorite.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/favorites/{id}
        ///     {
        ///        "UserId": "7138ec7e-d694-4333-8118-8791abe50fb3",
        ///        "MovieId": "aabf57da-014a-4cd8-8fc3-d07bd45042ca"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putFavoriteModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user or favorite could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> PutFavorite(string id, PutFavoriteModel putFavoriteModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid favoriteId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutFavorite", "400");
                }
                await _favoriteRepository.PutFavorite(id, putFavoriteModel);

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
        /// Deletes a favorite
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/favorites/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The user or favorite could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> DeleteFavorite(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid favoriteId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "DeleteFavorite", "400");
                }

                await _favoriteRepository.DeleteFavorite(id);

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
