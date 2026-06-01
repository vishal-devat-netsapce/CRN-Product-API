using DTOModel;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoleRepository  : IRoleRepository
    {
        private readonly ProductsContext _productsContext;
        public RoleRepository(ProductsContext productsContext)
        {
            _productsContext =productsContext;
        }
        public async Task<ApiResponse> AddRole(RoleDTO roleDTO)
        {
            bool isExist = await _productsContext.Roles
                .AnyAsync(x => x.RoleName == roleDTO.RoleName);

            if (isExist)
            {
                return new ApiResponse("400", false, null, "Role already exists.");
            }

            Role role = new Role
            {
                RoleName = roleDTO.RoleName
            };

            await _productsContext.Roles.AddAsync(role);
            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, role, "Role added successfully.");
        }

        public async Task<ApiResponse> GetRoleById(int roleId)
        {
            var role = await _productsContext.Roles
                .FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null)
            {
                return new ApiResponse("404", false, null, "Role not found.");
            }

            return new ApiResponse("200", true, role, "Role fetched successfully.");
        }

        public async Task<ApiResponse> GetAllRoles()
        {
            var roles = await _productsContext.Roles
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            return new ApiResponse("200", true, roles, "Roles fetched successfully.");
        }

        public async Task<ApiResponse> UpdateRole(UpdateRoleDTO roleDTO)
        {
            var role = await _productsContext.Roles
                .FirstOrDefaultAsync(x => x.Id == roleDTO.Id);

            if (role == null)
            {
                return new ApiResponse("404", false, null, "Role not found.");
            }

            bool isExist = await _productsContext.Roles
                .AnyAsync(x => x.RoleName == roleDTO.RoleName && x.Id != roleDTO.Id);

            if (isExist)
            {
                return new ApiResponse("400", false, null, "Role name already exists.");
            }

            role.RoleName = roleDTO.RoleName;

            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, role, "Role updated successfully.");
        }

        public async Task<ApiResponse> DeleteRole(int roleId)
        {
            var role = await _productsContext.Roles
                .FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null)
            {
                return new ApiResponse("404", false, null, "Role not found.");
            }

            bool isUsed = await _productsContext.Users
                .AnyAsync(x => x.RoleId == roleId);

            if (isUsed)
            {
                return new ApiResponse("400", false, null, "This role is assigned to users, so it cannot be deleted.");
            }

            _productsContext.Roles.Remove(role);
            await _productsContext.SaveChangesAsync();

            return new ApiResponse("200", true, null, "Role deleted successfully.");
        }
    }
}
