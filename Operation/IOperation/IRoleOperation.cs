using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    public interface IRoleOperation
    {
        Task<ApiResponse> AddRole(RoleDTO roleDTO);

        Task<ApiResponse> GetRoleById(int roleId);

        Task<ApiResponse> GetAllRoles();

        Task<ApiResponse> UpdateRole(UpdateRoleDTO roleDTO);

        Task<ApiResponse> DeleteRole(int roleId);
    }
}
