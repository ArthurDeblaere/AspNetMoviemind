using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.api.Entities;
using websolutionsproject.models.Roles;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.api.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly MovieMindContext _context;
        private readonly RoleManager<Role> _roleManager;

        public RoleRepository(MovieMindContext context, RoleManager<Role> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<List<GetRoleModel>> GetRoles()
        {
            List<GetRoleModel> roles = await _context.Roles
                .Select(x => new GetRoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .AsNoTracking()
                .ToListAsync();

            return roles;
        }

        public async Task<GetRoleModel> GetRole(string id)
        {
            GetRoleModel role = await _context.Roles
                .Select(x => new GetRoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (role == null)
            {
                throw new EntityException("Role not found", this.GetType().Name, "GetRole", "404");
            }
            return role;
        }

        public async Task PutRole(string id, PutRoleModel putRoleModel)
        {
            try
            {
                Role role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

                if (role == null)
                {
                    throw new EntityException("Role not found", this.GetType().Name, "PutRole", "404");
                }

                role.Name = putRoleModel.Name;
                role.Description = putRoleModel.Description;

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (!result.Succeeded)
                {
                    throw new IdentityException(result.Errors.First().Description, this.GetType().Name, "PutRole", "400");
                }
            }
            catch (MovieMindException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DatabaseException(e.InnerException.Message, this.GetType().Name, "PutRole", "400");
            }
        }
    }
}
