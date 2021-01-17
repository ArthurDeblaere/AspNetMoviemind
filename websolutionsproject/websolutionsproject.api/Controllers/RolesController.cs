using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.api.Repositories;
using websolutionsproject.models.Roles;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Get a list of all roles.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/roles
        ///
        /// </remarks>
        /// <returns>List of GetRoleModel</returns>
        /// <response code="200">Returns the list of roles</response>
        /// <response code="404">No roles were found</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<List<GetRoleModel>>> GetRoles()
        {
            return await _roleRepository.GetRoles();
        }


        /// <summary>
        /// Get details of an role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/roles/{id}
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <returns>An GetRoleModel</returns>
        /// <response code="200">Returns the role</response>
        /// <response code="404">The role could not be found</response> 
        /// <response code="400">The id is not a valid Guid</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetRoleModel>> GetRole(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid reviewId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "GetRole", "400");
                }

                return await _roleRepository.GetRole(id);
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
        /// Updates an role.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/roles/{id}
        ///     {
        ///        "Name": "Editor",
        ///        "Description": "EditorAtLarge"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>   
        /// <param name="putRoleModel"></param>   
        /// <response code="204">Returns no content</response>
        /// <response code="404">The role could not be found</response> 
        /// <response code="400">The id is not a valid Guid or something went wrong while saving into the database</response> 
        /// <response code="401">Unauthorized - Invalid JWT token</response> 
        /// <response code="403">Forbidden - Required role assignment is missing</response> 
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutRole(string id, PutRoleModel putRoleModel)
        {
            try
            {
                if (!Guid.TryParse(id, out Guid reviewId))
                {
                    throw new GuidException("Invalid id", this.GetType().Name, "PutRole", "400");
                }

                await _roleRepository.PutRole(id, putRoleModel);

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
